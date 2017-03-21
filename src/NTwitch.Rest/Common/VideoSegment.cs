using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class VideoSegment
    {
        [JsonProperty("duration")]
        public uint Duration { get; internal set; }
        [JsonProperty("offset")]
        public uint Offset { get; internal set; }
    }
}
