using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class Clip
    {
        [JsonProperty("broadcaster")]
        public User Broadcaster { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("curator")]
        public User Curator { get; set; }
        [JsonProperty("duration")]
        public double Duration { get; set; }
        [JsonProperty("embed_html")]
        public string EmbedHtml { get; set; }
        [JsonProperty("embed_url")]
        public string EmbedUrl { get; set; }
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("thumbnails")]
        public Dictionary<string, string> Thumbnails { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("tracking_id")]
        public ulong TrackingId { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("views")]
        public uint Views { get; set; }
        [JsonProperty("vod")]
        public Video Vod { get; set; }
    }
}
