using Newtonsoft.Json;
using System;

namespace NTwitch.Rest.API
{
    internal class User : SimpleUser
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
