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
        }
        
        public Task ConnectAsync(ulong userId)
        {
            if (TokenHelper.TryGetToken(this, userId, out RestTokenInfo info))
                throw new MissingScopeException("chat_login");
            if (!info.Authorization.Scopes.Contains("chat_login"))
                throw new MissingScopeException("chat_login");

            _chat = new ChatApiClient(_config, info.Username, info.Token);
            return _chat.ConnectAsync();
        }

        public Task DisconnectAsync()
            => _chat.DisconnectAsync();

        Task ITwitchClient.ConnectAsync()
            => throw new NotImplementedException();
        Task ITwitchClient.LoginAsync(string token)
            => throw new NotSupportedException();
    }
}
