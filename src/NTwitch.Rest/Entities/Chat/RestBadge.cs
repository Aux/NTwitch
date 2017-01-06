using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestBadge : IEntity, IBadge
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public string AlphaUrl { get; internal set; }
        [JsonProperty("")]
        public string ImageUrl { get; internal set; }
        [JsonProperty("")]
        public string Name { get; internal set; }
        [JsonProperty("")]
        public string SvgUrl { get; internal set; }

        internal RestBadge(TwitchRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
