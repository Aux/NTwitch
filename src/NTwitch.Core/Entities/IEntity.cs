using System;

namespace NTwitch
{
    public interface IEntity<TId>
        where TId : IEquatable<TId>
    {
        ITwitchClient Client { get; }
        TId Id { get; }
    }
}
