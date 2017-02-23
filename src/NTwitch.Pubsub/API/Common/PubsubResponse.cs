using Newtonsoft.Json;

namespace NTwitch.Pubsub
{
    internal class PubsubResponse
    {
        [JsonProperty("type")]
        public string Type { get; }
        [JsonProperty("nonce")]
        public string Nonce { get; }
        [JsonProperty("error")]
        public string Error { get; }

        [JsonConstructor]
        public PubsubResponse(string type, string nonce, string error = null)
        {
            Type = type;
            Nonce = nonce;
            Error = error;
        }
    }
}
