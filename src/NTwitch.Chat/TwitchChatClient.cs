using NTwitch.Rest;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : BaseRestClient, ITwitchClient
    {
        public ChatApiClient ChatClient => _chat;

        private ChatApiClient _chat;
        private TwitchChatConfig _config;

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config) : base(config)
        {
            _config = config;
            _chat = new ChatApiClient(config);
        }

        public Task<RestTokenInfo> LoginAsync(string token)
            => RestLoginAsync(token);

        public Task ConnectAsync()
        {
            if (Tokens.Count() != 1)
                throw new InvalidOperationException("You must log in as a single user to use implicit connection.");

            var auth = TokenInfos.First();
            return ConnectAsync(auth);
        }
        
        public async Task ConnectAsync(RestTokenInfo auth)
        {
            await _chat.ConnectAsync();
            await _chat.AuthorizeAsync(auth.Username, auth.Token);

            if (_config.RequestTags)
                await _chat.RequestTagsAsync().ConfigureAwait(false);
            if (_config.RequestCommands)
                await _chat.RequestCommandsAsync().ConfigureAwait(false);
            if (_config.RequestMembership)
                await _chat.RequestMembershipAsync().ConfigureAwait(false);
        }

        public Task DisconnectAsync()
            => _chat.DisconnectAsync();

        // Channels
        public Task JoinChannelAsync(string name)
            => _chat.JoinChannelAsync(name);
        public Task LeaveChannelAsync(string name)
            => _chat.PartChannelAsync(name);
        
        // ITwitchClient
        Task ITwitchClient.LoginAsync(string token)
            => throw new NotImplementedException();
    }
}
