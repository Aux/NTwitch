using Newtonsoft.Json;

namespace NTwitch
{
    public class ModifyChannelParams
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("delay")]
        public int Delay { get; set; }
        [JsonProperty("channel_feed_enabled")]
        public bool FeedEnabled { get; set; }
    }
}
