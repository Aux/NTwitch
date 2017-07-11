using NTwitch.Chat.API;
using NTwitch.Chat.Queue;
using NTwitch.Rest;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : BaseTwitchClient, ITwitchClient
    {
        private readonly SemaphoreSlim _stateLock;
        private readonly ConnectionManager _connection;
        private readonly ConcurrentQueue<long> _heartbeatTimes;
        private readonly Logger _chatLogger;

        private Task _heartbeatTask;
        private long _lastMessageTime;
        private int _heartbeatInterval;

        internal new TwitchChatApiClient ApiClient => base.ApiClient as TwitchChatApiClient;

        /// <summary> Gets the current connection state of this client. </summary>
        public ConnectionState ConnectionState => _connection.State;
        /// <summary> Gets the estimated round-trip latency, in milliseconds, to the chat server. </summary>
        public int Latency { get; private set; }

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config) 
            :base(config, CreateApiClient(config))
        {
            _stateLock = new SemaphoreSlim(1, 1);
            _chatLogger = LogManager.CreateLogger("Chat");
            _heartbeatTimes = new ConcurrentQueue<long>();
            _heartbeatInterval = config.HeartbeatInterval;

            _connection = new ConnectionManager(_stateLock, _chatLogger, config.ConnectionTimeout, 
                OnConnectingAsync, OnDisconnectingAsync, x => ApiClient.Disconnected += x);
            _connection.Connected += async () => await _connectedEvent.InvokeAsync().ConfigureAwait(false);
            _connection.Disconnected += async (ex, r) => await _disconnectedEvent.InvokeAsync(ex).ConfigureAwait(false);

            ApiClient.SentChatMessage += async cmd => await _chatLogger.DebugAsync($"Sent {cmd}").ConfigureAwait(false);
            ApiClient.ReceivedChatEvent += ProcessMessageAsync;

            CurrentUserLeft += async n => await _chatLogger.InfoAsync($"Left {n}").ConfigureAwait(false);
            CurrentUserJoined += async n => await _chatLogger.InfoAsync($"Joined {n}").ConfigureAwait(false);
            LatencyUpdated += async (old, val) => await _chatLogger.InfoAsync($"Latency = {val} ms").ConfigureAwait(false);
        }

        private static TwitchChatApiClient CreateApiClient(TwitchChatConfig config)
            => new TwitchChatApiClient(config.RestClientProvider, config.SocketClientProvider, config.CacheClientProvider, config.MessageCacheSize, config.ClientId, TwitchConfig.UserAgent, config.SocketHost);

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
            await _chatLogger.DebugAsync("Connecting ApiClient").ConfigureAwait(false);
            await ApiClient.ConnectAsync().ConfigureAwait(false);

            await ApiClient.SendIdentifyAsync(TokenInfo.Username, null).ConfigureAwait(false);
            await ApiClient.RequestCommandsAsync(null).ConfigureAwait(false);
            await ApiClient.RequestMembershipAsync(null).ConfigureAwait(false);
            await ApiClient.RequestTagsAsync(null).ConfigureAwait(false);

            _heartbeatTask = RunHeartbeatAsync(_heartbeatInterval, _connection.CancelToken);
        }

        private async Task OnDisconnectingAsync(Exception ex)
        {
            await _chatLogger.DebugAsync("Disconnecting ApiClient").ConfigureAwait(false);
            await ApiClient.DisconnectAsync().ConfigureAwait(false);
            
            await _chatLogger.DebugAsync("Waiting for heartbeater").ConfigureAwait(false);
            var heartbeatTask = _heartbeatTask;
            if (heartbeatTask != null)
                await heartbeatTask.ConfigureAwait(false);
            _heartbeatTask = null;

            while (_heartbeatTimes.TryDequeue(out long time)) { }
            _lastMessageTime = 0;
        }
        
        // Channels
        public Task JoinChannelAsync(string name, RequestOptions options = null)
            => ApiClient.JoinChannelAsync(name, options);
        public Task LeaveChannelAsync(string name, RequestOptions options = null)
            => ApiClient.LeaveChannelAsync(name, options);
        
        private async Task ProcessMessageAsync(ChatResponse msg)
        {
            try
            {
                switch (msg.Command)
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
                        await _chatLogger.DebugAsync("Received Reconnect").ConfigureAwait(false);
                        _connection.Error(new Exception("Server requested a reconnect"));
                        break;
                    case "JOIN":
                        {
                            var model = JoinEvent.Create(msg);

                            if (TokenInfo.Username == model.UserName)
                                await _currentUserJoinedEvent.InvokeAsync(model.ChannelName).ConfigureAwait(false);
                            else
                                await _userJoinedEvent.InvokeAsync(model.ChannelName, model.UserName).ConfigureAwait(false);
                        }
                        break;
                    case "PART":
                        {
                            var model = PartEvent.Create(msg);

                            if (TokenInfo.Username == model.UserName)
                                await _currentUserLeftEvent.InvokeAsync(model.ChannelName).ConfigureAwait(false);
                            else
                                await _userLeftEvent.InvokeAsync(model.ChannelName, model.UserName).ConfigureAwait(false);
                        }
                        break;
                    case "PRIVMSG":
                        {
                            var model = MessageReceivedEvent.Create(msg);
                            var entity = ChatMessage.Create(this, model);
                            await _messageReceivedEvent.InvokeAsync(entity).ConfigureAwait(false);
                        }
                        break;
                    case "USERNOTICE":
                        {
                            var model = UserNoticeEvent.Create(msg);
                            var entity = ChatNoticeMessage.Create(this, model);
                            await _messageReceivedEvent.InvokeAsync(entity).ConfigureAwait(false);
                        }
                        break;
                    case "NOTICE":  // Missing
                        break;
                    case "USERSTATE":
                        break;
                    case "ROOMSTATE":
                        break;
                    case "MODE":
                        {
                            var model = ModeEvent.Create(msg);

                            if (model.Type == "+o")
                                await _moderatorAddedEvent.InvokeAsync(model.ChannelName, model.UserName).ConfigureAwait(false);
                            else
                                await _moderatorRemovedEvent.InvokeAsync(model.ChannelName, model.UserName).ConfigureAwait(false);
                        }
                        break;
                    case "CLEARCHAT":
                        {
                            var model = ClearChatEvent.Create(msg);
                            var channel = ChatSimpleChannel.Create(this, model);
                            var user = ChatSimpleUser.Create(this, model);
                            var options = new BanOptions(model.Reason, model.Duration);
                            await _userBannedEvent.InvokeAsync(channel, user, options).ConfigureAwait(false);
                        }
                        break;
                    case "HOSTTARGET":  // missing
                        break;
                    case "GLOBALUSERSTATE":  // missing
                        break;
                    case "CAP": // Request Acks
                        break;
                    case "353": // Channel Names
                        break;
                    case "366": // End of channel names
                        break;
                    case "001": case "002": case "003":
                    case "004": case "375": case "372":
                    case "376":
                        await _chatLogger.DebugAsync($"Ignored command `{msg.Command}`").ConfigureAwait(false);
                        break;
                    default:
                        await _chatLogger.WarningAsync($"Unknown command `{msg.Command}`").ConfigureAwait(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                await _chatLogger.ErrorAsync($"Error handling {msg.Command}", ex).ConfigureAwait(false);
            }
        }

        private async Task RunHeartbeatAsync(int intervalMillis, CancellationToken cancelToken)
        {
            try
            {
                await _chatLogger.DebugAsync("Heartbeat Started").ConfigureAwait(false);
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
                        await _chatLogger.WarningAsync("Heartbeat Errored", ex).ConfigureAwait(false);
                    }

                    await Task.Delay(intervalMillis, cancelToken).ConfigureAwait(false);
                }
                await _chatLogger.DebugAsync("Heartbeat Stopped").ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                await _chatLogger.DebugAsync("Heartbeat Stopped").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await _chatLogger.ErrorAsync("Heartbeat Errored", ex).ConfigureAwait(false);
            }
        }
    }
}
