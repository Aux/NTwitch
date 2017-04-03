using NTwitch.Rest;
using System.Threading.Tasks;
using System;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubClient : BasePubsubClient, ITwitchClient
    {
        public TwitchPubsubClient() : this(new TwitchPubsubConfig()) { }
        public TwitchPubsubClient(TwitchPubsubConfig config) : base(config) { }
        
        public Task<RestTokenInfo> LoginAsync(string token)
            => SocketLoginAsync(token);

        public Task ConnectAsync()
            => ConnectInternalAsync();
        public Task DisconnectAsync()
            => DisconnectInternalAsync();
        Task ITwitchClient.LoginAsync(string token)
            => throw new NotImplementedException();
    }
}
