using System;

namespace NTwitch
{
    public interface ISimpleUser : IEntity<ulong>, IEquatable<ISimpleUser>
    {
        string AvatarUrl { get; }
        string Name { get; }
        string DisplayName { get; }
    }
}
