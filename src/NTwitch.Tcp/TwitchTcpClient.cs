using NTwitch.Rest;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Tcp
{
    public class TwitchTcpClient : BaseRestClient, ITwitchClient
    {
        public TcpApiClient ChatClient => _chat;

        private TcpApiClient _chat;
        private TwitchTcpConfig _config;

        public TwitchTcpClient() : this(new TwitchTcpConfig()) { }
        public TwitchTcpClient(TwitchTcpConfig config) : base(config)
        {
            _config = config;
        }

        public async Task LoginAsync(string username, string token)
        {
            await RestLoginAsync(TokenType.Oauth, token);

            _chat = new TcpApiClient(_config, username, token);
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
