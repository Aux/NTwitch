using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestVideo : IEntity, IVideo
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public ulong BroadcastId { get; internal set; }
        [JsonProperty("")]
        public RestChannelSummary Channel { get; internal set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("")]
        public string Description { get; internal set; }
        [JsonProperty("")]
        public string DescriptionRaw { get; internal set; }
        [JsonProperty("")]
        public string Game { get; internal set; }
        [JsonProperty("")]
        public string Language { get; internal set; }
        [JsonProperty("")]
        public int Length { get; internal set; }
        [JsonProperty("")]
        public TwitchImage Preview { get; internal set; }
        [JsonProperty("")]
        public DateTime PublishedAt { get; internal set; }
        [JsonProperty("")]
        public string Status { get; internal set; }
        [JsonProperty("")]
        public string[] Tags { get; internal set; }
        [JsonProperty("")]
        public string Title { get; internal set; }
        [JsonProperty("")]
        public BroadcastType Type { get; internal set; }
        [JsonProperty("")]
        public string Url { get; internal set; }
        [JsonProperty("")]
        public string Viewable { get; internal set; }
        [JsonProperty("")]
        public int Views { get; internal set; }

        internal RestVideo(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        internal static RestVideo Create(BaseTwitchClient client, string json)
        {
            var video = new RestVideo(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, video);
            return video;
        }

        ITwitchClient IEntity.Client
            => Client;
        IChannelSummary IVideo.Channel
            => Channel;
    }
}
