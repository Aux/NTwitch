using System;

namespace NTwitch.Rest
{
    public class RestStream : RestEntity, IStream
    {
        public double AverageFps { get; }
        public RestChannel Channel { get; }
        public DateTime CreatedAt { get; }
        public int Delay { get; }
        public string Game { get; }
        public bool IsPlaylist { get; }
        public TwitchImage Preview { get; }
        public int VideoHeight { get; }
        public int Viewers { get; }

        public RestStream(TwitchRestClient client, ulong id) : base(client, id) { }

        //IStream
        IChannel IStream.Channel
            => Channel;
    }
}
