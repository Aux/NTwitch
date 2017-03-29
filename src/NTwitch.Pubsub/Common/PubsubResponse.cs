using Newtonsoft.Json;

namespace NTwitch.Pubsub
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

        public static PubsubResponse FromString(string msg)
            => JsonConvert.DeserializeObject<PubsubResponse>(msg);
    }
}
