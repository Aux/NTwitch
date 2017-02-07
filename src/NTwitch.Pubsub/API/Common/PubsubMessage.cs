using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    internal class PubsubMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public PubsubMessageData Data { get; set; }
    }
}
