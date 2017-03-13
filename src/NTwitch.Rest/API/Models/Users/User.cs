using Newtonsoft.Json;
using System;

namespace NTwitch.Rest.API
{
    internal class User
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; set; }
    }
}
