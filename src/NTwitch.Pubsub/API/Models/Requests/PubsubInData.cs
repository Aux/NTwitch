using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class PubsubInData
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        public T GetMessage<T>()
            => JsonConvert.DeserializeObject<T>(Message);
    }
}
