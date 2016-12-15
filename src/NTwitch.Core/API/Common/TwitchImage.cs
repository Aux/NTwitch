using Newtonsoft.Json;

namespace Twitch
{
    public class TwitchImage
    {
        [JsonProperty("large")]
        public string LargeUrl { get; }
        [JsonProperty("medium")]
        public string MediumUrl { get; }
        [JsonProperty("small")]
        public string SmallUrl { get; }
        [JsonProperty("template")]
        public string TemplateUrl { get; }
    }
}
