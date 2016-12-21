using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : ITwitchClient
    {
        private TwitchChatConfig _config;
        private string _token;

        public TwitchChatClient() : this(new TwitchChatConfig()) { }
        public TwitchChatClient(TwitchChatConfig config)
        {
            _config = config;
        }
        
        // ITwitchClient
        public ConnectionState ConnectionState { get; }
        
        public Task LoginAsync(string username, string token)
        {
            throw new NotImplementedException();
        }

        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
