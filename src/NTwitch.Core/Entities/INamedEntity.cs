using System;

namespace NTwitch
{
    public interface INamedEntity<TId> : IEntity<TId>
        where TId : IEquatable<TId>
    {
        string Name { get; }
    }
}
