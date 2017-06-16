using Newtonsoft.Json;

namespace NTwitch
{
    public class ModifyVideoParams
    {
        [JsonProperty("title", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty("game", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Game { get; set; }
        [JsonProperty("language", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Language { get; set; }
        [JsonProperty("tag_list", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Tags { get; set; }
    }
}
