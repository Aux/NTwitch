using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class Video
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        // Video
        [JsonProperty("title")]
        public Optional<string> Title { get; set; }
        [JsonProperty("description")]
        public Optional<string> Description { get; set; }
        [JsonProperty("description_html")]
        public Optional<string> DescriptionHtml { get; set; }
        [JsonProperty("broadcast_id")]
        public Optional<uint> BroadcastId { get; set; }
        [JsonProperty("broadcast_type")]
        public Optional<string> BroadcastType { get; set; }
        [JsonProperty("status")]
        public Optional<string> Status { get; set; }
        [JsonProperty("tag_list")]
        public Optional<string> Tags { get; set; }
        [JsonProperty("language")]
        public Optional<string> Language { get; set; }
        [JsonProperty("game")]
        public Optional<string> Game { get; set; }
        [JsonProperty("length")]
        public Optional<uint> Length { get; set; }
        [JsonProperty("views")]
        public Optional<uint> Views { get; set; }
        [JsonProperty("viewable")]
        public Optional<string> Viewable { get; set; }
        [JsonProperty("viewable_at")]
        public Optional<DateTime?> ViewableAt { get; set; }
        [JsonProperty("created_at")]
        public Optional<DateTime> CreatedAt { get; set; }
        [JsonProperty("published_at")]
        public Optional<DateTime> PublishedAt { get; set; }
        [JsonProperty("recorded_at")]
        public Optional<DateTime> RecordedAt { get; set; }
        [JsonProperty("animated_preview_url")]
        public Optional<string> AnimatedPreviewUrl { get; set; }
        [JsonProperty("fps")]
        public Optional<IReadOnlyDictionary<string, float>> Fps { get; set; }
        [JsonProperty("resolutions")]
        public Optional<IReadOnlyDictionary<string, string>> Resolutions { get; set; }

        [JsonProperty("preview")]
        public Optional<PreviewImage> Preview { get; set; }
        [JsonProperty("thumbnails")]
        public Optional<VideoThumbnail> Thumbnails { get; set; }
        [JsonProperty("channel")]
        public Optional<Channel> Channel { get; set; }
    }
}
