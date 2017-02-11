using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTwitch.Pubsub
{
    public class PubsubWhisperMessage : PubsubEntity<ulong>
    {
        [JsonProperty("")]
        public string Type { get; internal set; }
        //[JsonProperty("")]
        //public object Data { get; internal set; }
        [JsonProperty("")]
        public string ThreadId { get; internal set; }
        [JsonProperty("")]
        public string Body { get; internal set; }
        [JsonProperty("")]
        public uint SentTS { get; internal set; }
        [JsonProperty("")]
        public uint FromId { get; internal set; }
        [JsonProperty("")]
        public object Tags { get; internal set; }
        [JsonProperty("")]
        public object Recipient { get; internal set; }


        public PubsubWhisperMessage(TwitchPubsubClient client) : base(client) { }
    }
}
