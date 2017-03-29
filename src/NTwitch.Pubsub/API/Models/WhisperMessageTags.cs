using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    internal class WhisperMessageTags
    {
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("emotes")]
        public IEnumerable<object> Emotes { get; set; }
        [JsonProperty("badges")]
        public IEnumerable<WhisperMessageBadge> Badges { get; set; }
    }
}
