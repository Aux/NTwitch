using NTwitch.Rest;
using System.Threading;
using NTwitch.Chat.Queue;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using NTwitch.Chat.API;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : BaseTwitchClient, ITwitchClient
    {
        private readonly SemaphoreSlim _stateLock;
        private readonly Logger _chatLogger;

        internal new TwitchChatApiClient ApiClient => base.ApiClient as TwitchChatApiClient;

        /// <summary> Gets the current connection state of this client. </summary>
        public ConnectionState ConnectionState => ApiClient.ConnectionState;
        /// <summary> Gets the estimated round-trip latency, in milliseconds, to the chat server. </summary>
        public int Latency { get; private set; }

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config) 
            :base(config, CreateApiClient(config))
        {
            _stateLock = new SemaphoreSlim(1, 1);
            _chatLogger = LogManager.CreateLogger("Chat");

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
                DisconnectAsync().GetAwaiter().GetResult();
                ApiClient.Dispose();
            }
        }
        
        internal override async Task OnLogoutAsync()
        {
            await DisconnectAsync().ConfigureAwait(false);
        }

        public async Task ConnectAsync()
        {
            await ApiClient.ConnectAsync().ConfigureAwait(false);
            await ApiClient.SendIdentifyAsync(TokenInfo.Username, null).ConfigureAwait(false);

            await ApiClient.RequestCommandsAsync(null).ConfigureAwait(false);
            await ApiClient.RequestMembershipAsync(null).ConfigureAwait(false);
            await ApiClient.RequestTagsAsync(null).ConfigureAwait(false);
        }

        public async Task DisconnectAsync()
            => await ApiClient.DisconnectAsync().ConfigureAwait(false);

        // Channels
        public Task JoinChannelAsync(string name, RequestOptions options = null)
            => ApiClient.JoinChannelAsync(name, options);

        // Users
        
        private async Task ProcessMessageAsync(ChatResponse msg)
        {
            try
            {
                switch (msg.Command)
                {
                    case "PING":
                        await ApiClient.SendPingAsync(null).ConfigureAwait(false);
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
                            var model = JoinEvent.Create(msg);

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
                    case "MODE":
                        {
                            var model = ModeEvent.Create(msg);

                            if (model.Type == "+o")
                                await _moderatorAddedEvent.InvokeAsync(model.ChannelName, model.UserName).ConfigureAwait(false);
                            else
                                await _moderatorRemovedEvent.InvokeAsync(model.ChannelName, model.UserName).ConfigureAwait(false);
                        }
                        break;
                    case "NOTICE":  // Missing
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
                    case "USERSTATE":
                        break;
                    case "RECONNECT":  // missing
                        break;
                    case "ROOMSTATE":
                        break;
                    case "USERNOTICE":
                        {
                            var model = UserNoticeEvent.Create(msg);
                            var entity = ChatNoticeMessage.Create(this, model);
                            await _messageReceivedEvent.InvokeAsync(entity).ConfigureAwait(false);
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
                    case "366": // End of names
                        break;
                    case "001": case "002": case "003":
                    case "004": case "375": case "372":
                    case "376":
                        break;  // Skip all the useless motd stuff
                    default:
                        await _chatLogger.WarningAsync($"Unknown command {msg.Command}").ConfigureAwait(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                await _chatLogger.ErrorAsync($"Error handling {msg.Command}", ex).ConfigureAwait(false);
            }
        }
    }
}
