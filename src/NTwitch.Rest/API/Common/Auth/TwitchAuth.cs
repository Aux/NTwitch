using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class TwitchAuth
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; internal set; }
        [JsonProperty("scopes")]
        public IEnumerable<string> Scopes { get; internal set; }
    }
}
