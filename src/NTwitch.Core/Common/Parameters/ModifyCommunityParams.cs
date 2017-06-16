using Newtonsoft.Json;

namespace NTwitch
{
    public class ModifyCommunityParams
    {
        [JsonProperty("summary", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Summary { get; set; }
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty("rules", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Rules { get; set; }
        [JsonProperty("email", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Email { get; set; }
    }
}
