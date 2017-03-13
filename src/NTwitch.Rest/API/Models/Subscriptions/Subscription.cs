using Newtonsoft.Json;
using System;

namespace NTwitch.Rest.API
{
    public class Subscription
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
