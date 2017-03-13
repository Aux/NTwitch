using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class SimpleUser
    {
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }
}
