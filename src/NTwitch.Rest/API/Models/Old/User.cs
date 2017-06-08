using Newtonsoft.Json;
using System;

namespace NTwitch.Rest.API
{
    internal class User 
    {
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("start_timestamp")]
        public DateTime StartAt { get; set; }
        [JsonProperty("end_timestamp")]
        public DateTime EndAt { get; set; }
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
