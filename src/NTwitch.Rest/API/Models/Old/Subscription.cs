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

        // UserSub
        [JsonProperty("user")]
        public User User { get; set; }

        // ChannelSub
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
