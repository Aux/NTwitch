using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestPostEmote : IEntity, IPostEmote
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public int End { get; internal set; }
        [JsonProperty("")]
        public ulong SetId { get; internal set; }
        [JsonProperty("")]
        public int Start { get; internal set; }

        internal RestPostEmote(TwitchRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
