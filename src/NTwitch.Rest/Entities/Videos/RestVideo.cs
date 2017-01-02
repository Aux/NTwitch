using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestVideo : IEntity, IVideo
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public ulong BroadcastId { get; internal set; }
        public RestChannelSummary Channel { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public string Description { get; internal set; }
        public string DescriptionRaw { get; internal set; }
        public string Game { get; internal set; }
        public string Language { get; internal set; }
        public int Length { get; internal set; }
        public TwitchImage Preview { get; internal set; }
        public DateTime PublishedAt { get; internal set; }
        public string Status { get; internal set; }
        public string[] Tags { get; internal set; }
        public string Title { get; internal set; }
        public BroadcastType Type { get; internal set; }
        public string Url { get; internal set; }
        public string Viewable { get; internal set; }
        public int Views { get; internal set; }

        internal RestVideo(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
        IChannelSummary IVideo.Channel
            => Channel;
    }
}
