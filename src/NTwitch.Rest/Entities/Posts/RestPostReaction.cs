using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestPostReaction
    {
        [JsonProperty("count")]
        public int Count { get; internal set; }
        [JsonProperty("emote")]
        public string EmoteName { get; internal set; }
        [JsonProperty("user_ids")]
        public ulong[] UserIds { get; internal set; }
    }
}
