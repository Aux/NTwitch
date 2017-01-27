using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestPostReaction
    {
        [JsonProperty("")]
        public int Count { get; internal set; }
        [JsonProperty("")]
        public string EmoteName { get; internal set; }
        [JsonProperty("")]
        public string Name { get; internal set; }
        [JsonProperty("")]
        public ulong[] UserIds { get; internal set; }
    }
}
