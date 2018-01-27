using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class User
    {
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("display_name")]
        public Optional<string> Username { get; set; }

        // User
        [JsonProperty("type")]
        public Optional<string> Type { get; set; }
        [JsonProperty("bio")]
        public Optional<string> Bio { get; set; }
        [JsonProperty("created_at")]
        public Optional<DateTime> CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public Optional<DateTime> UpdatedAt { get; set; }
        [JsonProperty("logo")]
        public Optional<string> AvatarImageUrl { get; set; }

        // SelfUser
        [JsonProperty("email")]
        public Optional<string> Email { get; set; }
        [JsonProperty("email_verified")]
        public Optional<bool> IsVerified { get; set; }
        [JsonProperty("partnered")]
        public Optional<bool> IsPartnered { get; set; }
        [JsonProperty("twitter_connected")]
        public Optional<bool> IsTwitterConnected { get; set; }
        [JsonProperty("notifications")]
        public Optional<UserNotifications> Notifications { get; set; }

        // Banned
        [JsonProperty("start_timestamp")]
        public Optional<DateTime> StartTimestamp { get; set; }
        [JsonProperty("end_timestamp")]
        public Optional<DateTime> EndTimestamp { get; set; }

        // WhisperUser
        [JsonProperty("color")]
        public Optional<string> Color { get; set; }
        [JsonProperty("user_type")]
        public Optional<string> UserType { get; set; }
        [JsonProperty("profile_image")]
        public Optional<string> ProfileImageUrl { get; set; }
        [JsonProperty("turbo")]
        public Optional<bool> IsTurbo { get; set; }
        [JsonProperty("badges")]
        public Optional<IReadOnlyCollection<Badge>> Badges { get; set; }

        // Whisper Receiver
        [JsonProperty("id")]
        public Optional<ulong?> OptionalId { get; set; }
        [JsonProperty("username")]
        public Optional<string> WhisperUsername { get; set; }

        // Whisper Sender
        [JsonProperty("login")]
        public Optional<string> Login { get; set; }
    }
}
