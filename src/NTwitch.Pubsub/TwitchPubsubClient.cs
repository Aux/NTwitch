using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubClient : BasePubsubClient, ITwitchClient
    {
        public TwitchPubsubClient() : this(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) : base(config) { }

        public Task LoginAsync(TokenType type, string token)
            => SocketLoginAsync(type, token);
        
        Task ITwitchClient.ConnectAsync()
            => throw new NotSupportedException();
        Task ITwitchClient.DisconnectAsync()
            => throw new NotSupportedException();
    }
}
