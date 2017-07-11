using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    public class BitsBadge
    {
        [JsonProperty("new_version")]
        public uint Next { get; internal set; }
        [JsonProperty("previous_version")]
        public uint Previous { get; internal set; }
    }
}
