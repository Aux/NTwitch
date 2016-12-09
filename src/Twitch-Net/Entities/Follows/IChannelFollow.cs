using System;

namespace Twitch
{
    public interface IChannelFollow
    {
        DateTime CreatedAt { get; }
        string[] Links { get; }
        bool Notifications { get; }
        IChannel Channel { get; }
    }
}
