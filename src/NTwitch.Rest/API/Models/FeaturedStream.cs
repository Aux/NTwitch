using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class FeaturedStream
    {
        [JsonProperty("stream")]
        public Stream Stream { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("priority")]
        public int Priority { get; set; }
        [JsonProperty("scheduled")]
        public bool Scheduled { get; set; }
        [JsonProperty("sponsored")]
        public bool Sponsored { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
