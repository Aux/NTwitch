using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class Emote
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("start")]
        public int StartIndex { get; set; }
        [JsonProperty("end")]
        public int EndIndex { get; set; }
    }
}
