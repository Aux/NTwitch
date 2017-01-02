using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestPostEmote : IEntity, IPostEmote
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public int End { get; internal set; }
        public ulong SetId { get; internal set; }
        public int Start { get; internal set; }

        internal RestPostEmote(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
