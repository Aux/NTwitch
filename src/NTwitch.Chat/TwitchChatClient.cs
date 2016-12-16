using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient : ITwitchClient
    {
        private TwitchChatClientConfig _config;
        private string _token;
        
        public IEnumerable<ChatChannel> Channels { get; private set; }

        public TwitchChatClient() { }
        public TwitchChatClient(TwitchChatClientConfig config)
        {
            _config = config;
        }

        public async Task JoinChannel(string name)
        {
            await Task.Delay(1);
        }

        public async Task LeaveChannel(string name)
        {
            await Task.Delay(1);
        }

        // ITwitchClient
        public ConnectionState ConnectionState { get; }
        
        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }
        
        public Task LoginAsync(string clientid, string token = null)
        {
            throw new NotImplementedException();
        }
    }
}
