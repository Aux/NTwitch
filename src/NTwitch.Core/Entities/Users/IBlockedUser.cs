using System;

namespace NTwitch
{
    public interface IBlockedUser : IEntity
    {
        DateTime UpdatedAt { get; }
        IUser User { get; }
    }
}
