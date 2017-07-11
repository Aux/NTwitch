using Newtonsoft.Json;
using System;

namespace NTwitch.Pubsub.API
{
    internal class BaseEvent
    {
        [JsonProperty("user_name")]
        public string UserName { get; set; }
        [JsonProperty("channel_name")]
        public string ChannelName { get; set; }
        [JsonProperty("user_id")]
        public ulong UserId { get; set; }
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }
}
