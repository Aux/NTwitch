using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    public class PubsubMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public PubsubMessageData Data { get; set; }
    }
}
