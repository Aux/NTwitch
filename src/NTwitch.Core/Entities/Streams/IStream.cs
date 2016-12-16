using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IStream
    {
        DateTime CreatedAt { get; }
        uint Id { get; }
        string Game { get; }
        IChannel Channel { get; }
        TwitchImage Preview { get; }
        double AverageFps { get; }
        int Viewers { get; }
        int Delay { get; }
        int VideoHeight { get; }
        bool IsPlaylist { get; }
        string[] Links { get; }
    }
}
