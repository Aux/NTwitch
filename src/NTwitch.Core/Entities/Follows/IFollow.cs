using System;

namespace NTwitch
{
    public interface IFollow : IEntity<ulong>
    {
        DateTime CreatedAt { get; }
    }
}
