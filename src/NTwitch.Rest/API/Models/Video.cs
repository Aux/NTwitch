using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class Video
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("broadcast_id")]
        public uint BroadcastId { get; set; }
        [JsonProperty("broadcast_type")]
        public string BroadcastType { get; set; }
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }
        [JsonProperty("fps")]
        public Dictionary<string, float> Fps { get; set; }
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("length")]
        public uint Length { get; set; }
        [JsonProperty("muted_segments")]
        public IEnumerable<VideoSegment> MutedSegments { get; set; }
        [JsonProperty("previews")]
        public Dictionary<string, string> PreviewImages { get; set; }
        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }
        [JsonProperty("resolutions")]
        public Dictionary<string, string> Resolutions { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("tag_list")]
        public string Tags { get; set; }
        [JsonProperty("thumbnails")]
        public Dictionary<string, VideoThumbnail> Thumbnails { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("viewable")]
        public string Viewable { get; set; }
        [JsonProperty("viewable_at")]
        public DateTime? ViewableAt { get; set; }
        [JsonProperty("views")]
        public uint Views { get; set; }
    }
}
