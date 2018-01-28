using Newtonsoft.Json;
using System;

namespace NTwitch.Rest.API
{
    internal class Follow
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("notifications")]
        public bool Notifications { get; set; }

        // User Follow
        [JsonProperty("user")]
        public Optional<User> User { get; set; }

        // Channel Follow
        [JsonProperty("channel")]
        public Optional<Channel> Channel { get; set; }
    }
}
