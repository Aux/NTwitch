using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class SelfUser : User
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("email_verified")]
        public bool IsVerified { get; set; }
        [JsonProperty("partnered")]
        public bool IsPartner { get; set; }
        [JsonProperty("twitter_connected")]
        public bool IsTwitterConnected { get; set; }
        [JsonProperty("notifications")]
        public UserNotifications Notifications { get; set; }
    }
}
