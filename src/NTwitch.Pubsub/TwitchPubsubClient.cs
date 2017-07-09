using NTwitch.Pubsub.API;
using NTwitch.Rest;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient : BaseTwitchClient, ITwitchClient
    {
        private readonly SemaphoreSlim _stateLock;
        private readonly ConnectionManager _connection;
        private readonly ConcurrentQueue<long> _heartbeatTimes;
        private readonly Logger _pubsubLogger;

        private Task _heartbeatTask;
        private long _lastMessageTime;
        private int _heartbeatInterval;

        internal new TwitchPubsubApiClient ApiClient => base.ApiClient as TwitchPubsubApiClient;

        /// <summary> Gets the current connection state of this client. </summary>
        public ConnectionState ConnectionState => _connection.State;
        /// <summary> Gets the estimated round-trip latency, in milliseconds, to the chat server. </summary>
        public int Latency { get; private set; }

        public TwitchPubsubClient() : this(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) 
            :base(config, CreateApiClient(config))
        {
            _stateLock = new SemaphoreSlim(1, 1);
            _pubsubLogger = LogManager.CreateLogger("Pubsub");
            _heartbeatTimes = new ConcurrentQueue<long>();
            _heartbeatInterval = config.HeartbeatInterval;

            _connection = new ConnectionManager(_stateLock, _pubsubLogger, config.ConnectionTimeout,
                OnConnectingAsync, OnDisconnectingAsync, x => ApiClient.Disconnected += x);
            _connection.Connected += async () => await _connectedEvent.InvokeAsync().ConfigureAwait(false);
            _connection.Disconnected += async (ex, r) => await _disconnectedEvent.InvokeAsync(ex).ConfigureAwait(false);

            ApiClient.SentPubsubMessage += async cmd => await _pubsubLogger.DebugAsync($"Sent {cmd}").ConfigureAwait(false);
            ApiClient.ReceivedPubsubEvent += ProcessMessageAsync;

            LatencyUpdated += async (old, val) => await _pubsubLogger.InfoAsync($"Latency = {val} ms").ConfigureAwait(false);
        }

        private static TwitchPubsubApiClient CreateApiClient(TwitchPubsubConfig config)
            => new TwitchPubsubApiClient(config.RestClientProvider, config.WebSocketProvider, config.ClientId, TwitchConfig.UserAgent, config.WebSocketHost);

        internal override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopAsync().GetAwaiter().GetResult();
                ApiClient.Dispose();
            }
        }

        internal override async Task OnLogoutAsync()
        {
            await StopAsync().ConfigureAwait(false);
        }

        public async Task StartAsync()
            => await _connection.StartAsync().ConfigureAwait(false);
        public async Task StopAsync()
            => await _connection.StopAsync().ConfigureAwait(false);

        private async Task OnConnectingAsync()
        {
            await _pubsubLogger.DebugAsync("Connecting ApiClient").ConfigureAwait(false);
            await ApiClient.ConnectAsync().ConfigureAwait(false);
            
            _heartbeatTask = RunHeartbeatAsync(_heartbeatInterval, _connection.CancelToken);
        }

        private async Task OnDisconnectingAsync(Exception ex)
        {
            await _pubsubLogger.DebugAsync("Disconnecting ApiClient").ConfigureAwait(false);
            await ApiClient.DisconnectAsync().ConfigureAwait(false);

            await _pubsubLogger.DebugAsync("Waiting for heartbeater").ConfigureAwait(false);
            var heartbeatTask = _heartbeatTask;
            if (heartbeatTask != null)
                await heartbeatTask.ConfigureAwait(false);
            _heartbeatTask = null;

            while (_heartbeatTimes.TryDequeue(out long time)) { }
            _lastMessageTime = 0;
        }

        // General
        public async Task ListenAsync(string topic, params string[] topics)
        {
            var all = GetEnumerable(topic, topics);
            await ApiClient.ListenAsync(all, null).ConfigureAwait(false);
        }

        public async Task UnlistenAsync(string topic, params string[] topics)
        {
            var all = GetEnumerable(topic, topics);
            await ApiClient.UnlistenAsync(all, null).ConfigureAwait(false);
        }

        // Channels
        public async Task ListenBitsAsync(ulong channelId, params ulong[] channelIds)
        {
            var all = GetEnumerable(channelId, channelIds);
            await ApiClient.ListenBitsAsync(all).ConfigureAwait(false);
        }

        public async Task ListenVideoPlaybackAsync(ulong channelId, params ulong[] channelIds)
        {
            var all = GetEnumerable(channelId, channelIds);
            await ApiClient.ListenVideoPlaybackAsync(all).ConfigureAwait(false);
        }

        // Users
        public async Task ListenWhispersAsync(ulong userId, params ulong[] userIds)
        {
            var all = GetEnumerable(userId, userIds);
            await ApiClient.ListenWhispersAsync(all).ConfigureAwait(false);
        }

        private async Task ProcessMessageAsync(PubsubFrame<PubsubInData> msg)
        {
            switch (msg.Type)
            {
                case "PING":
                    await ApiClient.SendPingAsync(null).ConfigureAwait(false);
                    break;
                case "PONG":
                    if (_heartbeatTimes.TryDequeue(out long time))
                    {
                        int latency = (int)(Environment.TickCount - time);
                        int before = Latency;
                        Latency = latency;

                        await _latencyUpdatedEvent.InvokeAsync(before, latency).ConfigureAwait(false);
                    }
                    break;
                case "RECONNECT":
                    await _pubsubLogger.DebugAsync("Received Reconnect").ConfigureAwait(false);
                    _connection.Error(new Exception("Server requested a reconnect"));
                    break;
                case "MESSAGE":
                    string topic = msg.Data.Topic.Split('.').First();

                    switch (topic)
                    {
                        //case "channel-bits-events-v1":
                        //    break;
                        case "channel-subscribe-events-v1":
                            var model = msg.Data.GetMessage<Subscription>();
                            await _subscriptionReceived.InvokeAsync(PubsubSubscription.Create(this, model));
                            break;
                        //case "channel-commerce-events-v1":
                        //    break;
                        //case "whispers":
                        //    break;
                        default:
                            await _anonymousReceivedEvent.InvokeAsync(msg.Data.Message);
                            break;
                    }
                    break;
                default:
                    await _pubsubLogger.DebugAsync($"Received unhandled {msg.Type}").ConfigureAwait(false);
                    return;
            }
        }

        private async Task RunHeartbeatAsync(int intervalMillis, CancellationToken cancelToken)
        {
            try
            {
                await _pubsubLogger.DebugAsync("Heartbeat Started").ConfigureAwait(false);
                while (!cancelToken.IsCancellationRequested)
                {
                    var now = Environment.TickCount;

                    if (_heartbeatTimes.Count != 0 && (now - _lastMessageTime) > intervalMillis)
                    {
                        if (ConnectionState == ConnectionState.Connected)
                        {
                            _connection.Error(new Exception("Server missed last heartbeat"));
                            return;
                        }
                    }

                    _heartbeatTimes.Enqueue(now);
                    try
                    {
                        await ApiClient.SendPingAsync(null).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        await _pubsubLogger.WarningAsync("Heartbeat Errored", ex).ConfigureAwait(false);
                    }

                    await Task.Delay(intervalMillis, cancelToken).ConfigureAwait(false);
                }
                await _pubsubLogger.DebugAsync("Heartbeat Stopped").ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                await _pubsubLogger.DebugAsync("Heartbeat Stopped").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await _pubsubLogger.ErrorAsync("Heartbeat Errored", ex).ConfigureAwait(false);
            }
        }

        private IEnumerable<T> GetEnumerable<T>(T first, T[] array)
        {
            var enumerable = new HashSet<T>(array);
            enumerable.Add(first);
            return enumerable;
        }
    }
}
