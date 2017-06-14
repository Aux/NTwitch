using Newtonsoft.Json;

namespace NTwitch
{
    public class UserNotifications
    {
        [JsonProperty("push")]
        public bool IsPushEnabled { get; internal set; }
        [JsonProperty("email")]
        public bool IsEmailEnabled { get; internal set; }
    }
}
