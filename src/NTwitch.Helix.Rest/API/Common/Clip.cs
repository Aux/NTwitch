using Newtonsoft.Json;
using System;

namespace NTwitch.Helix.API
{
    public class Clip
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        // Create Clip
        [JsonProperty("edit_url")]
        public Optional<string> EditUrl { get; set; }

        // Get Clip
        [JsonProperty("url")]
        public Optional<string> Url { get; set; }
        [JsonProperty("embed_url")]
        public Optional<string> EmbedUrl { get; set; }
        [JsonProperty("broadcaster_id")]
        public Optional<ulong> BroadcasterId { get; set; }
        [JsonProperty("creator_id")]
        public Optional<ulong> CreatorId { get; set; }
        [JsonProperty("video_id")]
        public Optional<ulong> VideoId { get; set; }
        [JsonProperty("game_id")]
        public Optional<ulong> GameId { get; set; }
        [JsonProperty("language")]
        public Optional<string> Language { get; set; }
        [JsonProperty("title")]
        public Optional<string> Title { get; set; }
        [JsonProperty("view_count")]
        public Optional<int> ViewCount { get; set; }
        [JsonProperty("created_at")]
        public Optional<DateTime> CreatedAt { get; set; }
        [JsonProperty("thumbnail_url")]
        public Optional<string> ThumbnailImageUrl { get; set; }
    }
}
