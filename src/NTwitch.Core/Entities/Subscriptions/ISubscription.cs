using System;

namespace NTwitch
{
    public interface ISubscription : IEntity
    {
        DateTime CreatedAt { get; }
    }
}
