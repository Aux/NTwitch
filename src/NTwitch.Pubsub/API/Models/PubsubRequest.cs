using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class PubsubRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("data")]
        public PubsubRequestData Data { get; set; } = new PubsubRequestData();
    }
}
