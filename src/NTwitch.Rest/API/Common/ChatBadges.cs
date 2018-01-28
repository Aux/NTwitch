using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class ChatBadges
    {
        [JsonProperty("admin")]
        public IReadOnlyDictionary<string, string> Admin { get; set; }
        [JsonProperty("broadcaster")]
        public IReadOnlyDictionary<string, string> Broadcaster { get; set; }
        [JsonProperty("global_mod")]
        public IReadOnlyDictionary<string, string> GlobalMod { get; set; }
        [JsonProperty("mod")]
        public IReadOnlyDictionary<string, string> Mod { get; set; }
        [JsonProperty("staff")]
        public IReadOnlyDictionary<string, string> Staff { get; set; }
        [JsonProperty("subscriber")]
        public IReadOnlyDictionary<string, string> Subscriber { get; set; }
        [JsonProperty("turbo")]
        public IReadOnlyDictionary<string, string> Turbo { get; set; }
    }
}
