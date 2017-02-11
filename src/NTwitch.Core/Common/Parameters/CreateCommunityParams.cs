using Newtonsoft.Json;

namespace NTwitch
{
    public class CreateCommunityParams
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("rules")]
        public string Rules { get; set; }
    }
}
