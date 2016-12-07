using System;
using System.Threading.Tasks;

namespace Twitch.Chat
{
    public class TwitchChatClient : ITwitchClient
    {
        private TwitchChatClientConfig _config;
        private string _token;
        
        public TwitchChatClient() { }
        public TwitchChatClient(TwitchChatClientConfig config)
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
