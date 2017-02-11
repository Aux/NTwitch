using System;

namespace NTwitch
{
    public interface ISubscription : IEntity<ulong>
    {
        DateTime CreatedAt { get; }
    }
}
