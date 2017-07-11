using Newtonsoft.Json;
using NTwitch.Pubsub.API;
using NTwitch.Pubsub.Queue;
using NTwitch.Rest;
using NTwitch.Rest.API;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    internal class TwitchPubsubApiClient : TwitchRestApiClient
    {
        public event Func<string, Task> SentPubsubMessage { add { _sentPusbubMessageEvent.Add(value); } remove { _sentPusbubMessageEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _sentPusbubMessageEvent = new AsyncEvent<Func<string, Task>>();
        public event Func<PubsubFrame<PubsubInData>, Task> ReceivedPubsubEvent { add { _receivedPubsubEvent.Add(value); } remove { _receivedPubsubEvent.Remove(value); } }
        private readonly AsyncEvent<Func<PubsubFrame<PubsubInData>, Task>> _receivedPubsubEvent = new AsyncEvent<Func<PubsubFrame<PubsubInData>, Task>>();

        public event Func<Exception, Task> Disconnected { add { _disconnectedEvent.Add(value); } remove { _disconnectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Exception, Task>> _disconnectedEvent = new AsyncEvent<Func<Exception, Task>>();

        private CancellationTokenSource _connectCancelToken;
        private string _webSocketUrl;

        internal IWebSocketClient WebSocketClient { get; }
        internal ConcurrentDictionary<string, string> RequestNonces { get; }

        public ConnectionState ConnectionState { get; private set; }
        
        public TwitchPubsubApiClient(RestClientProvider restClientProvider, SocketClientProvider socketClientProvider, string clientId, string userAgent,
            string webSocketUrl, RetryMode defaultRetryMode = RetryMode.AlwaysRetry, JsonSerializer serializer = null)
            : base(restClientProvider, clientId, userAgent, serializer)
        {
            _webSocketUrl = webSocketUrl;
            WebSocketClient = socketClientProvider() as IWebSocketClient;
            RequestNonces = new ConcurrentDictionary<string, string>();

            WebSocketClient.TextMessage += async text =>
            {
                using (var reader = new StringReader(text))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var msg = _serializer.Deserialize<PubsubFrame<PubsubInData>>(jsonReader);
                    if (msg != null)
                        await _receivedPubsubEvent.InvokeAsync(msg).ConfigureAwait(false);
                }
            };
            WebSocketClient.Closed += async ex =>
            {
                await DisconnectAsync().ConfigureAwait(false);
                await _disconnectedEvent.InvokeAsync(ex).ConfigureAwait(false);
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connectCancelToken?.Dispose();
                    (WebSocketClient as IDisposable)?.Dispose();
                }
                _disposed = true;
            }
        }
        
        public async Task ConnectAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await ConnectInternalAsync().ConfigureAwait(false);
            }
            finally { _stateLock.Release(); }
        }

        internal override async Task ConnectInternalAsync()
        {
            if (LoginState != LoginState.LoggedIn)
                throw new InvalidOperationException("You must log in before connecting.");

            ConnectionState = ConnectionState.Connecting;
            try
            {
                _connectCancelToken = new CancellationTokenSource();
                if (WebSocketClient != null)
                    WebSocketClient.SetCancelToken(_connectCancelToken.Token);

                await WebSocketClient.ConnectAsync(_webSocketUrl).ConfigureAwait(false);
                ConnectionState = ConnectionState.Connected;
            }
            catch
            {
                await DisconnectInternalAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task DisconnectAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await DisconnectInternalAsync().ConfigureAwait(false);
            }
            finally { _stateLock.Release(); }
        }

        internal override async Task DisconnectInternalAsync()
        {
            if (ConnectionState == ConnectionState.Disconnected) return;
            ConnectionState = ConnectionState.Disconnecting;

            try { _connectCancelToken?.Cancel(false); }
            catch { }

            await WebSocketClient.DisconnectAsync().ConfigureAwait(false);

            ConnectionState = ConnectionState.Disconnected;
        }

        public Task SendSocketAsync(PubsubRequestBuilder builder, RequestOptions options = null)
            => SendSocketAsync(builder.ToString(), builder.GetPayload(), builder.GetNonce(), options);
        public async Task SendSocketAsync(string value, object payload, string nonce = null, RequestOptions options = null)
        {
            CheckLoginState();
            if (nonce == null)
                RequestNonces.TryAdd(nonce, value);

            byte[] bytes = null;
            if (payload != null)
                bytes = Encoding.UTF8.GetBytes(SerializeJson(payload));
            var request = new PubsubRequest(WebSocketClient, null, bytes, true, options);
            await request.SendAsync().ConfigureAwait(false);
            await _sentPusbubMessageEvent.InvokeAsync(value).ConfigureAwait(false);
        }
        
        // General
        public async Task SendPingAsync(RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new PubsubRequestBuilder("PING", includeNonce: false), options).ConfigureAwait(false);
        }

        public async Task ListenAsync(IEnumerable<string> topics, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            var builder = new PubsubRequestBuilder("LISTEN", AuthToken);
            builder.Topics.AddRange(topics);
            await SendSocketAsync(builder, options).ConfigureAwait(false);
        }

        public async Task UnlistenAsync(IEnumerable<string> topics, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            var builder = new PubsubRequestBuilder("UNLISTEN", AuthToken);
            builder.Topics.AddRange(topics);
            await SendSocketAsync(builder, options).ConfigureAwait(false);
        }

        // Channels
        public async Task ListenBitsAsync(IEnumerable<ulong> channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowAnyScope = true;
            await SendSocketAsync(new BitsRequest(channelIds, AuthToken), options).ConfigureAwait(false);
        }

        public async Task UnlistenBitsAsync(IEnumerable<ulong> channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new BitsRequest(channelIds, AuthToken, false), options).ConfigureAwait(false);
        }

        public async Task ListenSubscriptionsAsync(IEnumerable<ulong> channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("channel_subscriptions");
            await SendSocketAsync(new SubscriptionsRequest(channelIds, AuthToken), options).ConfigureAwait(false);
        }

        public async Task UnlistenSubscriptionsAsync(IEnumerable<ulong> channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new SubscriptionsRequest(channelIds, AuthToken, false), options).ConfigureAwait(false);
        }

        public async Task ListenCommerceAsync(IEnumerable<ulong> channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AllowAnyScope = true;
            await SendSocketAsync(new CommerceRequest(channelIds, AuthToken), options).ConfigureAwait(false);
        }

        public async Task UnlistenCommerceAsync(IEnumerable<ulong> channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new CommerceRequest(channelIds, AuthToken, false), options).ConfigureAwait(false);
        }

        public async Task ListenVideoPlaybackAsync(IEnumerable<ulong> channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new VideoPlaybackRequest(channelIds, AuthToken), options).ConfigureAwait(false);
        }

        public async Task UnlistenVideoPlaybackAsync(IEnumerable<ulong> channelIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new VideoPlaybackRequest(channelIds, AuthToken, false), options).ConfigureAwait(false);
        }

        // Chat
        public async Task ListenWhispersAsync(IEnumerable<ulong> userIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            options.AddRequiredScopes("chat_login");
            await SendSocketAsync(new WhispersRequest(userIds, AuthToken), options).ConfigureAwait(false);
        }

        public async Task UnlistenWhispersAsync(IEnumerable<ulong> userIds, RequestOptions options = null)
        {
            options = RequestOptions.CreateOrClone(options);
            await SendSocketAsync(new WhispersRequest(userIds, AuthToken, false), options).ConfigureAwait(false);
        }
    }
}
