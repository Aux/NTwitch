using System;

namespace NTwitch
{
    public interface IBlockedUser : IEntity<ulong>
    {
        IUser User { get; }
        DateTime UpdatedAt { get; }
    }
}
