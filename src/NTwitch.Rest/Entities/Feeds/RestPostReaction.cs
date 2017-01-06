using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestPostReaction : IEntity, IPostReaction
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public int Count { get; internal set; }
        [JsonProperty("")]
        public string EmoteName { get; internal set; }
        [JsonProperty("")]
        public string Name { get; internal set; }
        [JsonProperty("")]
        public ulong[] UserIds { get; internal set; }

        internal RestPostReaction(TwitchRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
