using System;

namespace NTwitch
{
    public interface ISimpleUser : INamedEntity<ulong>, IEquatable<ISimpleUser>
    {
        string AvatarUrl { get; }
        string DisplayName { get; }
    }
}
