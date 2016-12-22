using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.WebSocket
{
    public partial class TwitchSocketClient : ITwitchClient
    {
        private SocketApiClient SocketClient { get; }
        public string SocketUrl { get; }

        public TwitchSocketClient() : this(new TwitchSocketConfig()) { }
        public TwitchSocketClient(TwitchSocketConfig config)
        {
            SocketUrl = config.SocketUrl;
        }
        
        // ITwitchClient
        public ConnectionState ConnectionState { get; } = ConnectionState.Disconnected;

        public Task LoginAsync(string clientid, string token = null)
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
