using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class TwitchRestClient : BaseRestClient, ITwitchClient
    {
        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) : base(config) { }

        public Task LoginAsync(AuthMode type, string token)
            => RestLoginAsync(type, token);

        Task ITwitchClient.ConnectAsync()
            => throw new NotSupportedException();
        Task ITwitchClient.DisconnectAsync()
            => throw new NotSupportedException();
    }
}
