using System;
using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class Follower
    {
        [JsonProperty("from_id")]
        public ulong FromId { get; set; }
        [JsonProperty("to_id")]
        public ulong ToId { get; set; }
        [JsonProperty("followed_at")]
        public DateTime FollowedAt { get; set; }
    }
}
