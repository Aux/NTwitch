using System;

namespace NTwitch.Rest
{
    public class RestVideo : RestEntity, IVideo
    {
        public ulong BroadcastId { get; }
        public RestChannelSummary Channel { get; }
        public DateTime CreatedAt { get; }
        public string Description { get; }
        public string DescriptionRaw { get; }
        public string Game { get; }
        public string Language { get; }
        public int Length { get; }
        public TwitchImage Preview { get; }
        public DateTime PublishedAt { get; }
        public string Status { get; }
        public string[] Tags { get; }
        public string Title { get; }
        public BroadcastType Type { get; }
        public string Url { get; }
        public string Viewable { get; }
        public int Views { get; }

        public RestVideo(TwitchRestClient client, ulong id) : base(client, id) { }

        //IVideo
        IChannelSummary IVideo.Channel
            => Channel;
    }
}
