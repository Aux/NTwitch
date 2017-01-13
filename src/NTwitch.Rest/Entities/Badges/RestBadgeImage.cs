using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestBadgeImage
    {
        [JsonProperty("alpha")]
        public string AlphaUrl { get; internal set; }
        [JsonProperty("image")]
        public string ImageUrl { get; internal set; }
        [JsonProperty("svg")]
        public string SvgUrl { get; internal set; }
    }
}
