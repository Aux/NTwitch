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
        public TwitchPubsubClient(TwitchPubsubConfig config)
        {
            _host = config.PubsubUrl;
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
