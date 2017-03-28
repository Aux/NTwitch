using Newtonsoft.Json;
using System;

namespace NTwitch.Pubsub.API
{
    internal class WhisperMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public WhisperMessageData Data { get; set; }
        [JsonProperty("thread_id")]
        public string ThreadId { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("sent_ts")]
        public DateTime SentTimestamp { get; set; }
        [JsonProperty("from_id")]
        public ulong FromId { get; set; }
        [JsonProperty("tags")]
        public WhisperMessageTags Tags { get; set; }
        [JsonProperty("recipient")]
        public WhisperUser Recipient { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

    }
}
