using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubClient : BasePubsubClient, ITwitchClient
    {
        public TwitchPubsubClient() : this(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) : base(config) { }
        
        public Task LoginAsync(AuthMode type, string token)
            => SocketLoginAsync(type, token);

        public Task ConnectAsync()
            => ConnectInternalAsync();
        public Task DisconnectAsync()
            => DisconnectInternalAsync();
    }
}
