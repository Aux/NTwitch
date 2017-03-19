using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class ModifyCommunityParams
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("rules")]
        public string Rules { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
