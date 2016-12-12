using System;

namespace Twitch.Rest
{
    public class RestStream : IStream
    {
        public double AverageFps { get; }
        public IChannel Channel { get; }
        public DateTime CreatedAt { get; }
        public int Delay { get; }
        public string Game { get; }
        public uint Id { get; }
        public bool IsPlaylist { get; }
        public string[] Links { get; }
        public TwitchImage Preview { get; }
        public int VideoHeight { get; }
        public int Viewers { get; }
    }
}
