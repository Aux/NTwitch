using System;

namespace Twitch
{
    public interface IFollow
    {
        DateTime CreatedAt { get; }
        string[] Links { get; }
        bool Notifications { get; }
    }
}
