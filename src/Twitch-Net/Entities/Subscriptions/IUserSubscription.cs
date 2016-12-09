using System;

namespace Twitch
{
    public interface IUserSubscription
    {
        DateTime CreatedAt { get; }
        string Id { get; }
        IUser User { get; }
        string[] Links { get; }
    }
}
