using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class ModifyChannelParams
    {
        [JsonProperty("status")]
        public string Status { get; set; } = null;
        [JsonProperty("game")]
        public string Game { get; set; } = null;
        [JsonProperty("delay")]
        public string Delay { get; set; } = null;
        [JsonProperty("channel_feed_enabled")]
        public bool? IsFeedEnabled { get; set; } = null;
    }
}
