using System;

namespace NTwitch
{
    public interface IFollow
    {
        DateTime CreatedAt { get; }
        string[] Links { get; }
        bool Notifications { get; }
    }
}
