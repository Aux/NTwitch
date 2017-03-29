using Newtonsoft.Json;
using System;

namespace NTwitch.Pubsub.API
{
    internal class BitsMessage
    {
        [JsonProperty("badge_entitlement")]
        public BadgeEntitlement Title { get; set; }
        [JsonProperty("bits_used")]
        public uint BitsUsed { get; set; }
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [JsonProperty("channel_name")]
        public string ChannelName { get; set; }
        [JsonProperty("chat_message")]
        public string ChatMessage { get; set; }
        [JsonProperty("context")]
        public string Context { get; set; }
        [JsonProperty("message_id")]
        public string MessageId { get; set; }
        [JsonProperty("message_type")]
        public string MessageType { get; set; }
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("total_bits_used")]
        public uint TotalBitsUsed { get; set; }
        [JsonProperty("user_id")]
        public ulong UserId { get; set; }
        [JsonProperty("user_name")]
        public string UserName { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
