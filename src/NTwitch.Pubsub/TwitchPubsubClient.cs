using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient : BaseTwitchClient, ITwitchClient
    {
        public ConnectionState ConnectionState { get; } = ConnectionState.Disconnected;
        public SocketApiClient SocketClient { get; }

        public TwitchPubsubClient() : this(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) : base(config)
        {
            
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
