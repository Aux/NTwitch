using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class TwitchNotifications
    {
        [JsonProperty("")]
        public bool IsEmailEnabled { get; internal set; }
        [JsonProperty("")]
        public bool IsPushEnabled { get; internal set; }
    }
}
