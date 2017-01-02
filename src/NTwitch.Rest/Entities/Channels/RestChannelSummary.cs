using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestChannelSummary : IEntity, IChannelSummary
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public string DisplayName { get; internal set; }
        public string Name { get; internal set; }

        internal RestChannelSummary(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
