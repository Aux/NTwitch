using Newtonsoft.Json;
using System;

namespace NTwitch.Pubsub.API
{
    internal class Subscription
    {
        [JsonProperty("user_name")]
        public string Username { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("channel_name")]
        public string ChannelName { get; set; }
        [JsonProperty("user_id")]
        public ulong UserId { get; set; }
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
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
