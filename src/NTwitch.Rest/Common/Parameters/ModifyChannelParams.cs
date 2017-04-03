using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class ModifyChannelParams
    {
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Status { get; set; } = null;
        [JsonProperty("game", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Game { get; set; } = null;
        [JsonProperty("delay", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Delay { get; set; } = null;
        [JsonProperty("channel_feed_enabled", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsFeedEnabled { get; set; } = null;
    }
}
