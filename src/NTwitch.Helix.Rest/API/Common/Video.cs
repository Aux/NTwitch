using System;
using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class Video
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("user_id")]
        public ulong UserId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }
        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
        [JsonProperty("view_count")]
        public int ViewCount { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("duration")]
        public TimeSpan Duration { get; set; }
    }
}
