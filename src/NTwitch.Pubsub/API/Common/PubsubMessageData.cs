using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    public class PubsubMessageData
    {
        [JsonProperty("topics")]
        public string Topic { get; set; }
        [JsonProperty("message")]
        public object Content { get; set; }
    }
}
