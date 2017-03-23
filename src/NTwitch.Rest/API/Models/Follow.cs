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
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
