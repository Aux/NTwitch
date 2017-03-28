using Newtonsoft.Json;

namespace NTwitch.Pubsub.API
{
    internal class BitsCollection
    {
        [JsonProperty("data")]
        public BitsMessage Data { get; set; }
    }
}
