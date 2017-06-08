using Newtonsoft.Json;
using System;

namespace NTwitch
{
    public class CreateVideoParams
    {
        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty("game", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Game { get; set; }
        [JsonProperty("language", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Language { get; set; }
        [JsonProperty("tag_list", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Tags { get; set; }
        [JsonProperty("viewable", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Viewable { get; set; }
        [JsonProperty("viewable_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime ViewableAt { get; set; }
    }
}
