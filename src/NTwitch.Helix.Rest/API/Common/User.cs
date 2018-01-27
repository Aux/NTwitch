using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class User
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }

        // User
        [JsonProperty("login")]
        public string Name { get; set; }
        [JsonProperty("display_name")]
        public Optional<string> Username { get; set; }
        [JsonProperty("type")]
        public Optional<UserType> Type { get; set; }
        [JsonProperty("broadcaster_type")]
        public Optional<BroadcasterType> BroadcasterType { get; set; }
        [JsonProperty("description")]
        public Optional<string> Description { get; set; }
        [JsonProperty("profile_image_url")]
        public Optional<string> ProfileImageUrl { get; set; }
        [JsonProperty("offline_image_url")]
        public Optional<string> OfflineImageUrl { get; set; }
        [JsonProperty("view_count")]
        public Optional<string> ViewCount { get; set; }

        // SelfUser
        [JsonProperty("email")]
        public Optional<string> Email { get; set; }
    }
}
