using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class PubsubFrame<T>
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("nonce", NullValueHandling = NullValueHandling.Ignore)]
        public string Nonce { get; set; }
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        public TMessage GetData<TMessage>()
            => JsonConvert.DeserializeObject<TMessage>(Data.ToString());
    }
}
