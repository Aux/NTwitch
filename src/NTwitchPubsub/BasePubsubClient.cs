using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class BasePubsubClient : BaseRestClient
    {
        public PubsubApiClient PubsubClient => _pubsub;
        
        private PubsubApiClient _pubsub;
        private TwitchPubsubConfig _config;
        
        public BasePubsubClient(TwitchPubsubConfig config) : base(config)
        {
            _config = config;
        }

        internal async Task SocketLoginAsync(AuthMode type, string token)
        {
            await RestLoginAsync(type, token);
            _pubsub = new PubsubApiClient(_config, Logger, type, token);
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
