using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestEmote : IEntity, IEmote
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public IEnumerable<IEmoteImage> Images { get; internal set; }
        public string Name { get; internal set; }

        internal RestEmote(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
