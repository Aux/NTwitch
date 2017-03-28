using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class PubsubResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public PubsubResponseData Data { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
