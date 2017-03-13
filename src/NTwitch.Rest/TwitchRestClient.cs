using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class TwitchRestClient : BaseRestClient, ITwitchClient
    {
        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) : base(config) { }

        public Task LoginAsync(TokenType type, string token)
            => LoginInternalAsync(type, token);

        Task ITwitchClient.ConnectAsync()
            => throw new NotImplementedException();
        Task ITwitchClient.DisconnectAsync()
            => throw new NotImplementedException();
    }
}
