using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestPostEmote
    {
        [JsonProperty("")]
        public int End { get; internal set; }
        [JsonProperty("")]
        public ulong SetId { get; internal set; }
        [JsonProperty("")]
        public int Start { get; internal set; }
    }
}
