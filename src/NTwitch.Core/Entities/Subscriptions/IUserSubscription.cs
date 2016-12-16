using System;

namespace NTwitch
{
    public interface IUserSubscription : ISubscription
    {
        IUser User { get; }
    }
}
