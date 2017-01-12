using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient : BaseRestClient, ITwitchClient
    {
        public PubsubClient Client => _pubsub;

        private PubsubClient _pubsub;
        private string _host;

        public TwitchPubsubClient() : this(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) : base(config)
        {
            _host = config.PubsubUrl;
        }

        public async Task LoginAsync(string clientid, string token = null)
        {
            await LoginInternalAsync(clientid, token);
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
