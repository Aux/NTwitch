using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class Video : Error
    {
        // SimpleVideo
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        // Video
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }
        [JsonProperty("broadcast_id")]
        public uint BroadcastId { get; set; }
        [JsonProperty("broadcast_type")]
        public string BroadcastType { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("tag_list")]
        public string Tags { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("length")]
        public uint Length { get; set; }
        [JsonProperty("views")]
        public uint Views { get; set; }
        [JsonProperty("viewable")]
        public string Viewable { get; set; }
        [JsonProperty("viewable_at")]
        public DateTime? ViewableAt { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }
        [JsonProperty("recorded_at")]
        public DateTime RecordedAt { get; set; }
        [JsonProperty("animated_preview_url")]
        public string AnimatedPreviewUrl { get; set; }
        [JsonProperty("fps")]
        public Dictionary<string, float> Fps { get; set; }
        [JsonProperty("resolutions")]
        public Dictionary<string, string> Resolutions { get; set; }

        [JsonProperty("preview")]
        public PreviewImage Preview { get; set; }
        [JsonProperty("thumbnails")]
        public VideoThumbnail Thumbnails { get; set; }
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
