using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestVideo : RestEntity<string>
    {
        [JsonProperty("broadcast_id")]
        public ulong BroadcastId { get; internal set; }
        [JsonProperty("broadcast_type")]
        public BroadcastType Type { get; internal set; }
        [JsonProperty("channel")]
        public RestChannelSummary Channel { get; internal set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("description")]
        public string Description { get; internal set; }
        [JsonProperty("description_html")]
        public string DescriptionRaw { get; internal set; }
        [JsonProperty("game")]
        public string GameName { get; internal set; }
        [JsonProperty("language")]
        public string Language { get; internal set; }
        [JsonProperty("length")]
        public int Length { get; internal set; }
        [JsonProperty("preview")]
        public TwitchImage Preview { get; internal set; }
        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; internal set; }
        [JsonProperty("status")]
        public string Status { get; internal set; }
        [JsonProperty("tag_list")]
        public string Tags { get; internal set; }
        [JsonProperty("title")]
        public string Title { get; internal set; }
        [JsonProperty("url")]
        public string Url { get; internal set; }
        [JsonProperty("viewable")]
        public string Viewable { get; internal set; }
        [JsonProperty("views")]
        public int ViewTotal { get; internal set; }
        
        internal RestVideo(BaseRestClient client) : base(client) { }
        
        internal static RestVideo Create(BaseRestClient client, string json)
        {
            var video = new RestVideo(client);
            JsonConvert.PopulateObject(json, video);
            return video;
        }
    }
}
