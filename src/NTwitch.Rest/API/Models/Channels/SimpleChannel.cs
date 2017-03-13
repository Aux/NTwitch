using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    public class SimpleChannel
    {
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }
}
