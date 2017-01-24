using System;

namespace NTwitch
{
    public interface IBlockedUser : IEntity
    {
        IUser User { get; }
        DateTime UpdatedAt { get; }
    }
}
