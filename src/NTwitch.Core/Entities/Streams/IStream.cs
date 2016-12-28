using System;

namespace NTwitch
{
    public interface IStream : IEntity
    {
        double AverageFps { get; }
        IChannel Channel { get; }
        DateTime CreatedAt { get; }
        int Delay { get; }
        string Game { get; }
        bool IsPlaylist { get; }
        TwitchImage Preview { get; }
        int VideoHeight { get; }
        int Viewers { get; }
    }
}
