using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    internal class SocketRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public SocketData Data { get; set; }
        [JsonProperty("nonce")]
        string Nonce { get; set; }
    }
}
