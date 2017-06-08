using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class PreviewImage
    {
        [JsonProperty("small")]
        public string Small { get; set; }
        [JsonProperty("medium")]
        public string Medium { get; set; }
        [JsonProperty("large")]
        public string Large { get; set; }
        [JsonProperty("template")]
        public string Template { get; set; }
    }
}
