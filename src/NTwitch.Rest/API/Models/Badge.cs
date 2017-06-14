using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class Badge
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
