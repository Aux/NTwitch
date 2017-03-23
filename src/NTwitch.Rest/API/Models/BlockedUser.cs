using Newtonsoft.Json;
using System;

namespace NTwitch.Rest.API
{
    internal class BlockedUser
    {
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
