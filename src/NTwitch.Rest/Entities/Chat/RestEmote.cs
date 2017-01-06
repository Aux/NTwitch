using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestEmote : IEntity, IEmote
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public IEnumerable<IEmoteImage> Images { get; internal set; }
        [JsonProperty("")]
        public string Name { get; internal set; }

        internal RestEmote(TwitchRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
