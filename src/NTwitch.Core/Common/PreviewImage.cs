using Newtonsoft.Json;

namespace NTwitch
{
    internal class PreviewImage
    {
        [JsonProperty("small")]
        public string Small { get; internal set; }
        [JsonProperty("medium")]
        public string Medium { get; internal set; }
        [JsonProperty("large")]
        public string Large { get; internal set; }
        [JsonProperty("template")]
        public string Template { get; internal set; }
    }
}
