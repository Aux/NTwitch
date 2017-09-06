using System;

namespace NTwitch
{
    public interface ISimpleChannel : INamedEntity<ulong>, IEquatable<ISimpleChannel>
    {
        string DisplayName { get; }
    }
}
