using System;

namespace NTwitch
{
    public interface IBlock : IEntity
    {
        DateTime UpdatedAt { get; }
        IUser User { get; }
    }
}
