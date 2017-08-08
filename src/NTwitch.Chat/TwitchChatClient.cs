using NTwitch.Chat.API;
using NTwitch.Chat.Queue;
using NTwitch.Rest;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : BaseTwitchClient, ITwitchClient
    {
        private readonly SemaphoreSlim _stateLock;
        private readonly CacheManager _cache;
        private readonly ConnectionManager _connection;
        private readonly ConcurrentQueue<long> _heartbeatTimes;
        private readonly Logger _chatLogger;

        private Task _heartbeatTask;
        private long _lastMessageTime;
        private int _heartbeatInterval;

        internal new TwitchChatApiClient ApiClient => base.ApiClient as TwitchChatApiClient;
        internal CacheManager Cache => _cache;

        /// <summary> Gets the estimated round-trip latency, in milliseconds, to the chat server. </summary>
        public int Latency { get; private set; }
        /// <summary> Gets the current connection state of this client. </summary>
        public ConnectionState ConnectionState => _connection.State;
        /// <summary> All of the channels the current user is connected to. </summary>
        public IReadOnlyCollection<ChatSimpleChannel> Channels => _cache.Channels;

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config) 
            :base(config, CreateApiClient(config))
        {
            _stateLock = new SemaphoreSlim(1, 1);
            _chatLogger = LogManager.CreateLogger("Chat");
            _heartbeatTimes = new ConcurrentQueue<long>();
            _heartbeatInterval = config.HeartbeatInterval;

            _cache = new CacheManager(config.MessageCacheSize, config.CacheClientProvider);

            _connection = new ConnectionManager(_stateLock, _chatLogger, config.ConnectionTimeout, 
                OnConnectingAsync, OnDisconnectingAsync, x => ApiClient.Disconnected += x);
            _connection.Connected += async () => await _connectedEvent.InvokeAsync().ConfigureAwait(false);
            _connection.Disconnected += async (ex, r) => await _disconnectedEvent.InvokeAsync(ex).ConfigureAwait(false);

            ApiClient.SentChatMessage += async cmd => await _chatLogger.DebugAsync($"Sent {cmd}").ConfigureAwait(false);
            ApiClient.ReceivedChatEvent += ProcessMessageAsync;

            CurrentUserLeft += async n => await _chatLogger.InfoAsync($"Left {n}").ConfigureAwait(false);
            CurrentUserJoined += async n => await _chatLogger.InfoAsync($"Joined {n.Key}").ConfigureAwait(false);
            LatencyUpdated += async (old, val) => await _chatLogger.InfoAsync($"Latency = {val} ms").ConfigureAwait(false);
        }

        private static TwitchChatApiClient CreateApiClient(TwitchChatConfig config)
            => new TwitchChatApiClient(config.RestClientProvider, config.SocketClientProvider, config.ClientId, TwitchConfig.UserAgent, config.SocketHost);

        internal override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopAsync().GetAwaiter().GetResult();
                ApiClient.Dispose();
            }
        }

        internal override async Task OnLoginAsync(bool validateToken)
        {
            await base.OnLoginAsync(validateToken).ConfigureAwait(false);

            if (!validateToken) return;
            if (!TokenInfo.Authorization.Scopes.Contains("chat_login"))
                throw new MissingScopeException("chat_login");
        }

        internal override async Task OnLogoutAsync()
        {
            await base.OnLogoutAsync().ConfigureAwait(false);
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
                            
                            var channel = _cache.GetChannel(model.ChannelName);
                            var cacheChannel = new Cacheable<string, ChatSimpleChannel>(channel, model.ChannelName, channel != null);

                            var user = _cache.GetUser(model.UserName);
                            var cacheUser = new Cacheable<string, ChatSimpleUser>(user, model.UserName, user != null);

                            if (TokenInfo.Username == model.UserName)
                                await _currentUserJoinedEvent.InvokeAsync(cacheChannel).ConfigureAwait(false);
                            else
                                await _userJoinedEvent.InvokeAsync(cacheChannel, cacheUser).ConfigureAwait(false);
                        }
                        break;
                    case "PART":
                        {
                            var model = PartEvent.Create(msg);

                            var channel = _cache.GetChannel(model.ChannelName);
                            var cacheChannel = new Cacheable<string, ChatSimpleChannel>(channel, model.ChannelName, channel != null);

                            var user = _cache.GetUser(model.UserName);
                            var cacheUser = new Cacheable<string, ChatSimpleUser>(user, model.UserName, user != null);

                            if (TokenInfo.Username == model.UserName)
                            {
                                if (channel != null)
                                {
                                    _cache.RemoveChannel(channel.Id);
                                    _cache.RemoveUserState(channel.Name);
                                }
                                await _currentUserLeftEvent.InvokeAsync(cacheChannel).ConfigureAwait(false);
                            }
                            else
                            {
                                if (user != null)
                                    _cache.RemoveUser(user.Id);
                                await _userLeftEvent.InvokeAsync(cacheChannel, cacheUser).ConfigureAwait(false);
                            }
                        }
                        break;
                    case "PRIVMSG":
                        {
                            var model = MessageReceivedEvent.Create(msg);
                            var entity = ChatMessage.Create(this, model);

                            _cache.AddChannel(entity.Channel);
                            _cache.AddUser(entity.Author);
                            _cache.AddMessage(entity);

                            await _messageReceivedEvent.InvokeAsync(entity).ConfigureAwait(false);
                        }
                        break;
                    case "USERNOTICE":
                        {
                            var model = UserNoticeEvent.Create(msg);
                            var entity = ChatNoticeMessage.Create(this, model);

                            _cache.AddChannel(entity.Channel);
                            _cache.AddUser(entity.Author);
                            _cache.AddMessage(entity);

                            await _messageReceivedEvent.InvokeAsync(entity).ConfigureAwait(false);
                        }
                        break;
                    case "NOTICE":
                        {
                            var model = NoticeEvent.Create(msg);
                            var channel = _cache.GetChannel(model.ChannelName) as ChatChannel;
                            
                            switch (model.Id)
                            {
                                //case "already_banned":
                                //    break;
                                //case "already_emote_only_off":
                                //    break;
                                //case "already_emote_only_on":
                                //    break;
                                //case "already_r9k_off":
                                //    break;
                                //case "already_r9k_on":
                                //    break;
                                //case "already_subs_off":
                                //    break;
                                //case "already_subs_on":
                                //    break;
                                //case "bad_host_hosting":
                                //    break;
                                //case "bad_unban_no_ban":
                                //    break;
                                //case "ban_success":
                                //    break;
                                case "emote_only_off":
                                    channel.IsEmoteOnly = false;
                                    _cache.AddChannel(channel);
                                    await _channelUpdated.InvokeAsync(channel).ConfigureAwait(false);
                                    break;
                                case "emote_only_on":
                                    channel.IsEmoteOnly = true;
                                    _cache.AddChannel(channel);
                                    await _channelUpdated.InvokeAsync(channel).ConfigureAwait(false);
                                    break;
                                //case "host_off":
                                //    break;
                                //case "host_on":
                                //    break;
                                //case "hosts_remaining":
                                //    break;
                                //case "msg_channel_suspended":
                                //    break;
                                case "r9k_off":
                                    channel.IsR9k = false;
                                    _cache.AddChannel(channel);
                                    await _channelUpdated.InvokeAsync(channel).ConfigureAwait(false);
                                    break;
                                case "r9k_on":
                                    channel.IsR9k = true;
                                    _cache.AddChannel(channel);
                                    await _channelUpdated.InvokeAsync(channel).ConfigureAwait(false);
                                    break;
                                case "slow_off":
                                    channel.IsSlow = false;
                                    _cache.AddChannel(channel);
                                    await _channelUpdated.InvokeAsync(channel).ConfigureAwait(false);
                                    break;
                                case "slow_on":
                                    channel.IsSlow = true;
                                    _cache.AddChannel(channel);
                                    await _channelUpdated.InvokeAsync(channel).ConfigureAwait(false);
                                    break;
                                case "subs_off":
                                    channel.IsSubsOnly = false;
                                    _cache.AddChannel(channel);
                                    await _channelUpdated.InvokeAsync(channel).ConfigureAwait(false);
                                    break;
                                case "subs_on":
                                    channel.IsSubsOnly = true;
                                    _cache.AddChannel(channel);
                                    await _channelUpdated.InvokeAsync(channel).ConfigureAwait(false);
                                    break;
                                //case "timeout_success":
                                //    break;
                                //case "unban_success":
                                //    break;
                                //case "unrecognized_cmd":
                                //    break;
                                default:
                                    await _chatLogger.WarningAsync($"Unhandled NOTICE id `{model.Id}`").ConfigureAwait(false);
                                    break;
                            }
                        }
                        break;
                    case "USERSTATE":
                        {
                            var model = UserStateEvent.Create(msg);
                            _cache.AddUserState(model);
                        }
                        break;
                    case "ROOMSTATE":
                        {
                            var model = RoomStateEvent.Create(msg);
                            var channel = ChatChannel.Create(this, model);

                            _cache.AddChannel(channel);
                        }
                        break;
                    case "MODE":
                        {
                            var model = ModeEvent.Create(msg);

                            var userState = _cache.GetUserState(model.ChannelName);
                            if (userState != null)
                            {
                                userState.IsMod = model.Type == "+o";
                                _cache.AddUserState(userState);
                            }

                            var channel = _cache.GetChannel(model.ChannelName);
                            var cacheChannel = new Cacheable<string, ChatSimpleChannel>(channel, model.ChannelName, channel != null);

                            var user = _cache.GetUser(model.UserName);
                            var cacheUser = new Cacheable<string, ChatSimpleUser>(user, model.UserName, user != null);

                            if (model.Type == "+o")
                                await _moderatorAddedEvent.InvokeAsync(cacheChannel, cacheUser).ConfigureAwait(false);
                            else
                                await _moderatorRemovedEvent.InvokeAsync(cacheChannel, cacheUser).ConfigureAwait(false);
                        }
                        break;
                    case "CLEARCHAT":
                        {
                            var model = ClearChatEvent.Create(msg);
                            var channel = ChatSimpleChannel.Create(this, model);
                            var user = ChatSimpleUser.Create(this, model);
                            var options = new BanOptions(model.Reason, model.Duration);

                            _cache.AddChannel(channel);
                            _cache.AddUser(user);

                            await _userBannedEvent.InvokeAsync(channel, user, options).ConfigureAwait(false);
                        }
                        break;
                    case "HOSTTARGET":
                        {
                            var model = HostEvent.Create(msg);

                            var host = _cache.GetChannel(model.HostName);
                            var cacheHost = new Cacheable<string, ChatSimpleChannel>(host, model.HostName, host != null);

                            if (model.ChannelName == null)
                            {
                                await _hostingStopped.InvokeAsync(cacheHost, model.Viewers).ConfigureAwait(false);
                            } else
                            {
                                var channel = _cache.GetChannel(model.ChannelName);
                                var cacheChannel = new Cacheable<string, ChatSimpleChannel>(channel, model.ChannelName, channel != null);

                                await _hostingStarted.InvokeAsync(cacheHost, cacheChannel, model.Viewers).ConfigureAwait(false);
                            }
                        }
                        break;
                    case "353":
                        {
                            var model = NamesEvent.Create(msg);
                            _cache.AddNames(model.ChannelName, model.Names);
                        }
                        break;
                    case "GLOBALUSERSTATE": // Is never received
                    case "001": case "002": case "003":
                    case "004": case "366": case "375":
                    case "372": case "376": case "CAP":
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
