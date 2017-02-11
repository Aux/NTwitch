using Newtonsoft.Json;
using System;

namespace NTwitch.Pubsub
{
    public class PubsubBitsMessage : PubsubEntity<ulong>
    {
        [JsonProperty("time")]
        public DateTime Timestamp { get; internal set; }
        [JsonProperty("user_id")]
        public ulong UserId { get; internal set; }
        [JsonProperty("user_name")]
        public string UserName { get; internal set; }
        [JsonProperty("channel_id")]
        public uint ChannelId { get; internal set; }
        [JsonProperty("channel_name")]
        public string ChannelName { get; internal set; }
        [JsonProperty("chat_message")]
        public string Content { get; internal set; }
        [JsonProperty("bits_used")]
        public int BitsUsed { get; internal set; }
        [JsonProperty("total_bits_used")]
        public int BitsTotal { get; internal set; }
        [JsonProperty("context")]
        public string Context { get; internal set; }

        public PubsubBitsMessage(TwitchPubsubClient client) : base(client) { }
    }
}
