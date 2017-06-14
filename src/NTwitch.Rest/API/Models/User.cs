using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class User : Error
    {
        // SimpleUser
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        // User
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("logo")]
        public string AvatarUrl { get; set; }

        // SelfUser
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("email_verified")]
        public bool IsVerified { get; set; }
        [JsonProperty("partnered")]
        public bool IsPartnered { get; set; }
        [JsonProperty("twitter_connected")]
        public bool IsTwitterConnected { get; set; }
        [JsonProperty("notifications")]
        public UserNotifications Notifications { get; set; }

        // Banned
        [JsonProperty("start_timestamp")]
        public DateTime StartTimestamp { get; set; }
        [JsonProperty("end_timestamp")]
        public DateTime EndTimestamp { get; set; }
        
        // WhisperUser
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("user_type")]
        public string UserType { get; set; }
        [JsonProperty("profile_image")]
        public string ProfileImageUrl { get; set; }
        [JsonProperty("turbo")]
        public bool IsTurbo { get; set; }
        [JsonProperty("badges")]
        public IEnumerable<Badge> Badges { get; set; }

        // Whisper Receiver
        [JsonProperty("id")]
        public ulong? OptionalId { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        
        // Whisper Sender
        [JsonProperty("login")]
        public string Login { get; set; }
    }
}
