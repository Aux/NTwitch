using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    public class CheerInfo
    {
        [JsonProperty("backgrounds")]
        public IEnumerable<string> Backgrounds { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("scales")]
        public IEnumerable<double> Scales { get; set; }
        [JsonProperty("states")]
        public IEnumerable<string> States { get; set; }
        [JsonProperty("tiers")]
        public IEnumerable<Cheer> Tiers { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
