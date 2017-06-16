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

            LeftChannel += async n => await _chatLogger.InfoAsync($"Left {n}").ConfigureAwait(false);
            JoinedChannel += async n => await _chatLogger.InfoAsync($"Joined {n}").ConfigureAwait(false);
            LatencyUpdated += async (old, val) => await _chatLogger.InfoAsync($"Latency = {val} ms").ConfigureAwait(false);
        }

        private static TwitchChatApiClient CreateApiClient(TwitchChatConfig config)
            => new TwitchChatApiClient(config.RestClientProvider, config.SocketClientProvider, config.CacheClientProvider, config.ClientId, TwitchConfig.UserAgent, config.SocketHost);

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
            await Task.Delay(0);

            try
            {
                switch (msg.Command)
                {
                    case "PING":
                        await ApiClient.SendPingAsync(null).ConfigureAwait(false);
                        break;
                    case "JOIN":
                        await _joinedChannelEvent.InvokeAsync(msg.Parameters.First().Substring(1)).ConfigureAwait(false);
                        break;
                    case "PART":
                        await _leftChannelEvent.InvokeAsync(msg.Parameters.First().Substring(1)).ConfigureAwait(false);
                        break;
                    case "PRIVMSG":
                        {
                            var model = MessageReceivedEvent.Create(msg);
                            var entity = ChatMessage.Create(this, model);
                            await _messageReceivedEvent.InvokeAsync(entity).ConfigureAwait(false);
                            ApiClient.CacheClient.AddMessage(entity);
                        }
                        break;
                    case "MODE":  // missing
                        break;
                    case "NOTICE":  // missing
                        break;
                    case "CLEARCHAT":
                        break;
                    case "USERSTATE":
                        {
                            var model = UserStateEvent.Create(msg);
                            var entity = ChatSelfUser.Create(this, model);
                            ApiClient.CacheClient.AddUser(entity);
                        }
                        break;
                    case "RECONNECT":  // missing
                        break;
                    case "ROOMSTATE":
                        break;
                    case "USERNOTICE":
                        break;
                    case "HOSTTARGET":  // missing
                        break;
                    case "GLOBALUSERSTATE":  // missing
                        break;
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
