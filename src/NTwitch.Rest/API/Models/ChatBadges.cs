using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class ChatBadges
    {
        [JsonProperty("admin")]
        public Dictionary<string, string> Admin { get; set; }
        [JsonProperty("broadcaster")]
        public Dictionary<string, string> Broadcaster { get; set; }
        [JsonProperty("global_mod")]
        public Dictionary<string, string> GlobalMod { get; set; }
        [JsonProperty("mod")]
        public Dictionary<string, string> Mod { get; set; }
        [JsonProperty("staff")]
        public Dictionary<string, string> Staff { get; set; }
        [JsonProperty("subscriber")]
        public Dictionary<string, string> Subscriber { get; set; }
        [JsonProperty("turbo")]
        public Dictionary<string, string> Turbo { get; set; }
    }
}
