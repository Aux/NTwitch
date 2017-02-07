using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    internal class PubsubMessageData
    {
        [JsonProperty("topics")]
        public string Topic { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
