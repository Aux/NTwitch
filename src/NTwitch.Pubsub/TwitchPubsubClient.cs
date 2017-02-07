using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient : BaseRestClient, ITwitchClient
    {
        private SocketClient _socket;
        private string _host;
        
        public TwitchPubsubClient() : base(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) : base(config)
        {
            _host = config.PubsubUrl;
        }

        public async Task LoginAsync(TokenType type, string token)
        {
            await LoginInternalAsync(type, token);
            _socket = new SocketClient(_host, token);
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
