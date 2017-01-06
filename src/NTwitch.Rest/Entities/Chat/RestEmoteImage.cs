using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestEmoteImage : IEntity, IEmoteImage
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public int Height { get; internal set; }
        [JsonProperty("")]
        public string ImageUrl { get; internal set; }
        [JsonProperty("")]
        public ulong SetId { get; internal set; }
        [JsonProperty("")]
        public int Width { get; internal set; }

        internal RestEmoteImage(TwitchRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
