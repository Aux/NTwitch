using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.WebSocket
{
    public partial class BaseSocketClient : BaseRestClient
    {
        public SocketApiClient SocketClient => _socket;

        internal SocketApiClient _socket;

        private TwitchSocketConfig _config;
        
        public BaseSocketClient(TwitchSocketConfig config) : base(config)
        {
            _config = config;
        }

        internal async Task SocketLoginAsync(TokenType type, string token)
        {
            await RestLoginAsync(type, token);
            _socket = new SocketApiClient(_config, type, token);
        }

        internal Task ConnectInternalAsync()
        {
            throw new NotImplementedException();
        }

        internal Task DisconnectInternalAsync()
        {
            throw new NotImplementedException();
        }
    }
}
