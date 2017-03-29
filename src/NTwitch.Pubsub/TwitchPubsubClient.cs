using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubClient : BasePubsubClient, ITwitchClient
    {
        public TwitchPubsubClient() : this(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) : base(config) { }
        
        public Task LoginAsync(AuthMode type, string token)
            => SocketLoginAsync(type, token);

        public Task DisconnectAsync()
            => DisconnectInternalAsync();

        Task ITwitchClient.ConnectAsync()
            => throw new NotSupportedException();
    }
}
