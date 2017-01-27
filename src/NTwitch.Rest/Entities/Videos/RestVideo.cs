using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestVideo : RestEntity
    {
        [JsonProperty("")]
        public ulong BroadcastId { get; private set; }
        [JsonProperty("")]
        public RestChannelSummary Channel { get; private set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; private set; }
        [JsonProperty("")]
        public string Description { get; private set; }
        [JsonProperty("")]
        public string DescriptionRaw { get; private set; }
        [JsonProperty("")]
        public string Game { get; private set; }
        [JsonProperty("")]
        public string Language { get; private set; }
        [JsonProperty("")]
        public int Length { get; private set; }
        [JsonProperty("")]
        public TwitchImage Preview { get; private set; }
        [JsonProperty("")]
        public DateTime PublishedAt { get; private set; }
        [JsonProperty("")]
        public string Status { get; private set; }
        [JsonProperty("")]
        public string[] Tags { get; private set; }
        [JsonProperty("")]
        public string Title { get; private set; }
        [JsonProperty("")]
        public BroadcastType Type { get; private set; }
        [JsonProperty("")]
        public string Url { get; private set; }
        [JsonProperty("")]
        public string Viewable { get; private set; }
        [JsonProperty("")]
        public int Views { get; private set; }
        
        internal RestVideo(BaseRestClient client) : base(client) { }

        internal static RestVideo Create(BaseRestClient client, string json)
        {
            var video = new RestVideo(client);
            JsonConvert.PopulateObject(json, video);
            return video;
        }
    }
}
