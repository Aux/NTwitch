using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestBadge : IEntity, IBadge
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public string AlphaUrl { get; internal set; }
        public string ImageUrl { get; internal set; }
        public string Name { get; internal set; }
        public string SvgUrl { get; internal set; }

        internal RestBadge(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
