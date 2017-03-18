using NTwitch.Rest;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class TwitchChatClient : BaseRestClient, ITwitchClient
    {
        public ChatApiClient ChatClient => _chat;

        private ChatApiClient _chat;
        private TwitchChatConfig _config;

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config) : base(config)
        {
            _config = config;
        }

        public async Task LoginAsync(string username, string token)
        {
            await RestLoginAsync(TokenType.Oauth, token);

            _chat = new ChatApiClient(_config, username, token);
            if (!Token.Authorization.Scopes.Contains("chat_login"))
                throw new InvalidOperationException("This token does not have permission to login to chat.");
        }

        public Task ConnectAsync()
            => _chat.ConnectAsync();

        public Task DisconnectAsync()
            => _chat.DisconnectAsync();

        Task ITwitchClient.LoginAsync(TokenType type, string token)
            => throw new NotSupportedException();
    }
}
