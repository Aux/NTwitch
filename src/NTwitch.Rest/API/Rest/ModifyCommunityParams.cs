using Newtonsoft.Json;

namespace NTwitch.Rest
{
    internal class ModifyCommunityParams
    {
        [JsonProperty("summary")]
        public Optional<string> Summary { get; set; }
        [JsonProperty("description")]
        public Optional<string> Description { get; set; }
        [JsonProperty("rules")]
        public Optional<string> Rules { get; set; }
        [JsonProperty("email")]
        public Optional<string> Email { get; set; }
    }
}
