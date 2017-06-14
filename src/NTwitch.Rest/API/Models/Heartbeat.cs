using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class Heartbeat
    {
        [JsonProperty("identifier")]
        public string Id { get; set; }
    }
}
