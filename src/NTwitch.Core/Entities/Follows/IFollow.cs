using System;

namespace NTwitch
{
    public interface IFollow : IEntity
    {
        DateTime CreatedAt { get; }
    }
}
