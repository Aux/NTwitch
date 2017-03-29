using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    internal class WhisperUser
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("badges")]
        public IEnumerable<WhisperMessageBadge> Badges { get; set; }
    }
}
