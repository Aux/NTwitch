using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    internal class SocketResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
