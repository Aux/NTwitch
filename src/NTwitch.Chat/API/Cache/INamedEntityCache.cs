using System;

namespace NTwitch.Chat
{
    public interface INamedEntityCache<TKey, TEntity> : IEntityCache<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : INamedEntity<TKey>
    {
        TEntity Remove(string name);
        TEntity Get(string name);
    }
}
