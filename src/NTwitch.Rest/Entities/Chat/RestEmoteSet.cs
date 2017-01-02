using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestEmoteSet : IEntity, IEmoteSet
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public IEnumerable<IEmote> Emotes { get; internal set; }

        internal RestEmoteSet(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
