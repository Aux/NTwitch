using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class Team
    {
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("background")]
        public string Background { get; set; }
        [JsonProperty("banner")]
        public string BannerUrl { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("info")]
        public string Info { get; set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("users")]
        public IEnumerable<Channel> Channels { get; set; }
    }
}
