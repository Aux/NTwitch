using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestFeaturedStream : IEntity, IFeaturedStream
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public string ImageUrl { get; internal set; }
        [JsonProperty("")]
        public bool IsScheduled { get; internal set; }
        [JsonProperty("")]
        public bool IsSponsored { get; internal set; }
        [JsonProperty("")]
        public int Priority { get; internal set; }
        [JsonProperty("")]
        public RestStream Stream { get; internal set; }
        [JsonProperty("")]
        public string Text { get; internal set; }
        [JsonProperty("")]
        public string Title { get; internal set; }

        internal RestFeaturedStream(TwitchRestClient client)
        {
            Client = client;
        }

        internal static RestFeaturedStream Create(BaseTwitchClient client, string json)
        {
            var stream = new RestFeaturedStream(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, stream);
            return stream;
        }

        ITwitchClient IEntity.Client
            => Client;
        IStream IFeaturedStream.Stream
            => Stream;
    }
}
