using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestBadge : IBadge
    {
        [JsonProperty("")]
        public string AlphaUrl { get; internal set; }
        [JsonProperty("")]
        public string ImageUrl { get; internal set; }
        [JsonProperty("")]
        public string Name { get; internal set; }
        [JsonProperty("")]
        public string SvgUrl { get; internal set; }
    }
}
