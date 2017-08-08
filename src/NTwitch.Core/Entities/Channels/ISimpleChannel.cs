using System;

namespace NTwitch
{
    public interface ISimpleChannel : IEntity<ulong>, IEquatable<ISimpleChannel>
    {
        string Name { get; }
        string DisplayName { get; }
    }
}
