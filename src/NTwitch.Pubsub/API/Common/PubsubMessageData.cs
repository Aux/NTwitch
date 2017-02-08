using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    internal class PubsubMessageData<T>
    {
        [JsonProperty("topics")]
        public string Topic { get; set; }
        [JsonProperty("message")]
        public T Content { get; set; }
    }
}
