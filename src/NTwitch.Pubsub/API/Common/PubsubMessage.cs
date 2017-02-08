using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    internal class PubsubMessage<T>
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public PubsubMessageData<T> Data { get; set; }
    }
}
