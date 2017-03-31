using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class TwitchRestClient : BaseRestClient, ITwitchClient
    {
        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) : base(config) { }

        /// <summary> Authorize this client with a clientid or oauth token </summary>
        public Task LoginAsync(string token)
            => RestLoginAsync(token);

        Task ITwitchClient.ConnectAsync()
            => throw new NotSupportedException();
        Task ITwitchClient.DisconnectAsync()
            => throw new NotSupportedException();
    }
}
