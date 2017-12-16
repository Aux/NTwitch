using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NTwitch.Helix.Rest;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Helix.Queue
{
    internal class RequestBucket
    {
        private readonly object _lock;
        private readonly RequestQueue _queue;
        private int _semaphore;
        private DateTimeOffset? _resetTick;

        public string Id { get; private set; }
        public int WindowCount { get; private set; }
        public DateTimeOffset LastAttemptAt { get; private set; }

        public RequestBucket(RequestQueue queue, RestRequest request, string id)
        {
            _queue = queue;
            Id = id;

            _lock = new object();

            if (request.Options.IsClientBucket)
                WindowCount = ClientBucket.Get(request.Options.BucketId).WindowCount;
            else
                WindowCount = 1; //Only allow one request until we get a header back
            _semaphore = WindowCount;
            _resetTick = null;
            LastAttemptAt = DateTimeOffset.UtcNow;
        }

        static int nextId = 0;
        public async Task<Stream> SendAsync(RestRequest request)
        {
            int id = Interlocked.Increment(ref nextId);

            LastAttemptAt = DateTimeOffset.UtcNow;
            while (true)
            {
                await _queue.EnterGlobalAsync(id, request).ConfigureAwait(false);
                await EnterAsync(id, request).ConfigureAwait(false);

                RateLimitInfo info = default(RateLimitInfo);
                try
                {
                    var response = await request.SendAsync().ConfigureAwait(false);
                    info = new RateLimitInfo(response.Headers);

                    if (response.StatusCode < (HttpStatusCode)200 || response.StatusCode >= (HttpStatusCode)300)
                    {
                        switch (response.StatusCode)
                        {
                            case (HttpStatusCode)403:
                                UpdateRateLimit(id, request, info, true);

                                await _queue.RaiseRateLimitTriggered(Id, info).ConfigureAwait(false);
                                continue; //Retry
                            case HttpStatusCode.BadGateway: //502
                                if ((request.Options.RetryMode & RetryMode.Retry502) == 0)
                                    throw new HttpException(HttpStatusCode.BadGateway, null);

                                continue; //Retry
                            default:
                                string reason = null;
                                if (response.Stream != null)
                                {
                                    try
                                    {
                                        using (var reader = new StreamReader(response.Stream))
                                        using (var jsonReader = new JsonTextReader(reader))
                                        {
                                            var json = JToken.Load(jsonReader);
                                            try { reason = json.Value<string>("message"); } catch { };
                                        }
                                    }
                                    catch { }
                                }
                                throw new HttpException(response.StatusCode, reason);
                        }
                    }
                    else
                    {
                        return response.Stream;
                    }
                }
                catch (TimeoutException)
                {
                    if ((request.Options.RetryMode & RetryMode.RetryTimeouts) == 0)
                        throw;

                    await Task.Delay(500);
                    continue; //Retry
                }
                finally
                {
                    UpdateRateLimit(id, request, info, false);
                }
            }
        }

        private async Task EnterAsync(int id, RestRequest request)
        {
            int windowCount;
            DateTimeOffset? resetAt;
            bool isRateLimited = false;

            while (true)
            {
                if (DateTimeOffset.UtcNow > request.TimeoutAt || request.Options.CancelToken.IsCancellationRequested)
                {
                    if (!isRateLimited)
                        throw new TimeoutException();
                    else
                        throw new RateLimitedException();
                }

                lock (_lock)
                {
                    windowCount = WindowCount;
                    resetAt = _resetTick;
                }

                DateTimeOffset? timeoutAt = request.TimeoutAt;
                if (windowCount > 0 && Interlocked.Decrement(ref _semaphore) < 0)
                {
                    if (!isRateLimited)
                    {
                        isRateLimited = true;
                        await _queue.RaiseRateLimitTriggered(Id, null).ConfigureAwait(false);
                    }

                    if ((request.Options.RetryMode & RetryMode.RetryRatelimit) == 0)
                        throw new RateLimitedException();

                    if (resetAt.HasValue)
                    {
                        if (resetAt > timeoutAt)
                            throw new RateLimitedException();
                        int millis = (int)Math.Ceiling((resetAt.Value - DateTimeOffset.UtcNow).TotalMilliseconds);

                        if (millis > 0)
                            await Task.Delay(millis, request.Options.CancelToken).ConfigureAwait(false);
                    }
                    else
                    {
                        if ((timeoutAt.Value - DateTimeOffset.UtcNow).TotalMilliseconds < 500.0)
                            throw new RateLimitedException();

                        await Task.Delay(500, request.Options.CancelToken).ConfigureAwait(false);
                    }
                    continue;
                }

                break;
            }
        }

        private void UpdateRateLimit(int id, RestRequest request, RateLimitInfo info, bool is429)
        {
            if (WindowCount == 0)
                return;

            lock (_lock)
            {
                bool hasQueuedReset = _resetTick != null;
                if (info.Limit.HasValue && WindowCount != info.Limit.Value)
                {
                    WindowCount = info.Limit.Value;
                    _semaphore = info.Remaining.Value;
                }

                var now = DateTimeUtils.ToUnixSeconds(DateTimeOffset.UtcNow);
                DateTimeOffset? resetTick = null;

                if (info.Reset.HasValue)
                {
                    resetTick = info.Reset.Value.AddSeconds(info.Lag?.TotalSeconds ?? 1.0);
                    int diff = (int)(resetTick.Value - DateTimeOffset.UtcNow).TotalMilliseconds;
                }
                else if (request.Options.IsClientBucket && request.Options.BucketId != null)
                {
                    resetTick = DateTimeOffset.UtcNow.AddSeconds(ClientBucket.Get(request.Options.BucketId).WindowSeconds);
                }

                if (resetTick == null)
                {
                    WindowCount = 0; //No rate limit info, disable limits on this bucket (should only ever happen with a user token)
                    return;
                }

                if (!hasQueuedReset || resetTick > _resetTick)
                {
                    _resetTick = resetTick;
                    LastAttemptAt = resetTick.Value; //Make sure we dont destroy this until after its been reset

                    if (!hasQueuedReset)
                    {
                        var _ = QueueReset(id, (int)Math.Ceiling((_resetTick.Value - DateTimeOffset.UtcNow).TotalMilliseconds));
                    }
                }
            }
        }
        private async Task QueueReset(int id, int millis)
        {
            while (true)
            {
                if (millis > 0)
                    await Task.Delay(millis).ConfigureAwait(false);
                lock (_lock)
                {
                    millis = (int)Math.Ceiling((_resetTick.Value - DateTimeOffset.UtcNow).TotalMilliseconds);
                    if (millis <= 0) //Make sure we havent gotten a more accurate reset time
                    {
                        _semaphore = WindowCount;
                        _resetTick = null;
                        return;
                    }
                }
            }
        }
    }
}
