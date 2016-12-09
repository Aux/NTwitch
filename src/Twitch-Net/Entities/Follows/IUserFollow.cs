using System;

namespace Twitch
{
    public interface IUserFollow
    {
        DateTime CreatedAt { get; }
        string[] Links { get; }
        bool Notifications { get; }
        IUser User { get; }
    }
}
