using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class PubsubResponseData
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
