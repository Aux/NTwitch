using Newtonsoft.Json;
using System;

namespace NTwitch.Rest.API
{
    internal class Subscription
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
