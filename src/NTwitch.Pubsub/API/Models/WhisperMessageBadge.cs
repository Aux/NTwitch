using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class WhisperMessageBadge
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
