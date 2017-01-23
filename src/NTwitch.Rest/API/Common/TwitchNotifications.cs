using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class TwitchNotifications
    {
        [JsonProperty("email")]
        public bool IsEmailEnabled { get; internal set; }
        [JsonProperty("push")]
        public bool IsPushEnabled { get; internal set; }
    }
}
