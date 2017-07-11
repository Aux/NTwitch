using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class BitsMessageEvent
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("message_type")]
        public string MessageType { get; set; }
        [JsonProperty("message_id")]
        public string MessageId { get; set; }
        [JsonProperty("data")]
        public BitsMessageData Data { get; set; }
    }
}
