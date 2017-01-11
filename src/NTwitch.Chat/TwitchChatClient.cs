using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : BaseRestClient, ITwitchClient
    {
        public ChatClient Client => _chat;

        private ChatClient _chat;
        private string _host;

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config)
        {
            _host = config.ChatUrl;

            var user = new ChatUser(this);

            user.
        }
        
        public Task LoginAsync()
        {
            LoginInternalAsync("");
            throw new NotImplementedException();
        }

        public override Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public override Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
