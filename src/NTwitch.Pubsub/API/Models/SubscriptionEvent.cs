using Newtonsoft.Json;
using System;

namespace NTwitch.Pubsub.API
{
    internal class SubscriptionEvent : BaseEvent
    {
        [JsonProperty("sub_plan")]
        public string SubPlan { get; set; }
        [JsonProperty("sub_plan_name")]
        public string SubPlanName { get; set; }
        [JsonProperty("months")]
        public int Months { get; set; }
        [JsonProperty("context")]
        public string Context { get; set; }
        [JsonProperty("sub_message")]
        public Message Message { get; set; }
    }
}
