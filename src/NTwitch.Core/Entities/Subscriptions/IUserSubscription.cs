using System;

namespace Twitch
{
    public interface IUserSubscription : ISubscription
    {
        IUser User { get; }
    }
}
