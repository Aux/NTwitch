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
        [JsonProperty("sub_plan")]
        public int SubPlan { get; set; }
        [JsonProperty("sub_plan_name")]
        public string SubPlanName { get; set; }

        // User Sub
        [JsonProperty("user")]
        public Optional<User> User { get; set; }

        // Channel Sub
        [JsonProperty("channel")]
        public Optional<Channel> Channel { get; set; }
    }
}
