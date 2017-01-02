using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestStream : IEntity, IStream
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public double AverageFps { get; internal set; }
        public RestChannel Channel { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public int Delay { get; internal set; }
        public string Game { get; internal set; }
        public bool IsPlaylist { get; internal set; }
        public TwitchImage Preview { get; internal set; }
        public int VideoHeight { get; internal set; }
        public int Viewers { get; internal set; }

        internal RestStream(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
        IChannel IStream.Channel
            => Channel;
    }
}
