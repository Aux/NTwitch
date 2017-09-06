using System;
using System.Collections.Generic;

namespace NTwitch.Chat
{
    public interface IEntityCache<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>
    {
        IReadOnlyCollection<TEntity> Entities { get; }

        void Add(TKey id, TEntity entity);

        TEntity Remove(TKey id);
        TEntity Get(TKey id);
        TEntity GetOrAdd(TKey id, Func<TKey, TEntity> factory);
    }
}
