using Newtonsoft.Json;
using System;

namespace NTwitch.Pubsub.API
{
    internal class BitsMessageData
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
        [JsonProperty("chat_message")]
        public string Content { get; set; }
        [JsonProperty("bits_used")]
        public uint BitsUsed { get; set; }
        [JsonProperty("total_bits_used")]
        public uint TotalBitsUsed { get; set; }
        [JsonProperty("context")]
        public string Context { get; set; }
        [JsonProperty("badge_entitlement")]
        public BitsBadge Badge { get; set; }
    }
}
