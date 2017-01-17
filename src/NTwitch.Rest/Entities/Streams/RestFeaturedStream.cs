using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestFeaturedStream : StreamBase
    {
        [JsonProperty("")]
        public string ImageUrl { get; private set; }
        [JsonProperty("")]
        public bool IsScheduled { get; private set; }
        [JsonProperty("")]
        public bool IsSponsored { get; private set; }
        [JsonProperty("")]
        public int Priority { get; private set; }
        [JsonProperty("")]
        public RestStream Stream { get; private set; }
        [JsonProperty("")]
        public string Text { get; private set; }
        [JsonProperty("")]
        public string Title { get; private set; }

        public RestFeaturedStream(BaseRestClient client) : base(client) { }

        public static RestFeaturedStream Create(BaseRestClient client, string json)
        {
            var stream = new RestFeaturedStream(client);
            JsonConvert.PopulateObject(json, stream);
            return stream;
        }
    }
}
