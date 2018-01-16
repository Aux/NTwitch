using System;
using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class Broadcast
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("user_id")]
        public Optional<ulong> UserId { get; set; }
        [JsonProperty("game_id")]
        public Optional<ulong> GameId { get; set; }
        [JsonProperty("community_ids")]
        public Optional<ulong[]> CommunityIds { get; set; }
        [JsonProperty("type")]
        public Optional<string> Type { get; set; }
        [JsonProperty("title")]
        public Optional<string> Title { get; set; }
        [JsonProperty("viewer_count")]
        public Optional<int> ViewerCount { get; set; }
        [JsonProperty("started_at")]
        public Optional<DateTime> StartedAt { get; set; }
        [JsonProperty("language")]
        public Optional<string> Language { get; set; }
        [JsonProperty("thumbnail_url")]
        public Optional<string> ThumbnailImageUrl { get; set; }
    }
}
