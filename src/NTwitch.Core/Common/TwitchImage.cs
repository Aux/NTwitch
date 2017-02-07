using Newtonsoft.Json;

namespace NTwitch
{
    public class TwitchImage
    {
        [JsonProperty("large")]
        public string LargeUrl { get; internal set; }
        [JsonProperty("medium")]
        public string MediumUrl { get; internal set; }
        [JsonProperty("small")]
        public string SmallUrl { get; internal set; }
        [JsonProperty("template")]
        public string TemplateUrl { get; internal set; }
    }
}
