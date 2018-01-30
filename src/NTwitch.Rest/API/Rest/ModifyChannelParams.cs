using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class ModifyChannelParams
    {
        [JsonProperty("status")]
        public Optional<string> Status { get; set; }
        [JsonProperty("game")]
        public Optional<string> Game { get; set; }
        [JsonProperty("delay")]
        public Optional<string> Delay { get; set; }
        [JsonProperty("channel_feed_enabled")]
        public Optional<bool> IsFeedEnabled { get; set; }
    }
}
