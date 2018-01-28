using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class CheerInfo
    {
        [JsonProperty("backgrounds")]
        public IReadOnlyCollection<string> Backgrounds { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("scales")]
        public IReadOnlyCollection<double> Scales { get; set; }
        [JsonProperty("states")]
        public IReadOnlyCollection<string> States { get; set; }
        [JsonProperty("tiers")]
        public IReadOnlyCollection<Cheer> Tiers { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
