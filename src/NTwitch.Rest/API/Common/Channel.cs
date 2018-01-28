using Newtonsoft.Json;
using System;

namespace NTwitch.Rest.API
{
    internal class Channel
    {
        // Simple Channel
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("display_name")]
        public string Username { get; set; }

        // Channel
        [JsonProperty("mature")]
        public Optional<bool> IsMature { get; set; }
        [JsonProperty("status")]
        public Optional<string> Status { get; set; }
        [JsonProperty("broadcaster_language")]
        public Optional<string> BroadcasterLanguage { get; set; }
        [JsonProperty("game")]
        public Optional<string> Game { get; set; }
        [JsonProperty("language")]
        public Optional<string> Language { get; set; }
        [JsonProperty("created_at")]
        public Optional<DateTime> CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public Optional<DateTime> UpdatedAt { get; set; }
        [JsonProperty("logo")]
        public Optional<string> LogoUrl { get; set; }
        [JsonProperty("video_banner")]
        public Optional<string> VideoBannerUrl { get; set; }
        [JsonProperty("profile_banner")]
        public Optional<string> ProfileBannerImageUrl { get; set; }
        [JsonProperty("profile_banner_background_color")]
        public Optional<string> ProfileBannerBackgroundColor { get; set; }
        [JsonProperty("partner")]
        public Optional<bool> IsPartner { get; set; }
        [JsonProperty("url")]
        public Optional<string> Url { get; set; }
        [JsonProperty("views")]
        public Optional<uint> ViewCount { get; set; }
        [JsonProperty("followers")]
        public Optional<uint> FollowerCount { get; set; }

        // Self Channel
        [JsonProperty("email")]
        public Optional<string> Email { get; set; }
        [JsonProperty("stream_key")]
        public Optional<string> StreamKey { get; set; }
    }
}
