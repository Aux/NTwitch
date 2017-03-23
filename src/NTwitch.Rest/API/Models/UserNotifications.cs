using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class UserNotifications
    {
        [JsonProperty("push")]
        public bool IsPushEnabled { get; set; }
        [JsonProperty("email")]
        public bool IsEmailEnabled { get; set; }
    }
}
