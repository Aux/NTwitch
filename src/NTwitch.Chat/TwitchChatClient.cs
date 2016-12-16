using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : ITwitchClient
    {
        private TwitchChatClientConfig _config;
        private string _token;

        public TwitchChatClient() : this(new TwitchChatClientConfig()) { }
        public TwitchChatClient(TwitchChatClientConfig config)
        {
            _config = config;
        }
        
        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }

        // ITwitchClient
        public ConnectionState ConnectionState { get; }
        
        public Task LoginAsync(string username, string token)
        {
            throw new NotImplementedException();
        }
    }
}
