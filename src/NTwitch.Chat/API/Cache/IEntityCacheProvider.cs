using System;

namespace NTwitch.Chat
{
    public interface IEntityCacheProvider
    {
        IEntityCache<TKey, TEntity> CreateCache<TKey, TEntity>(int cacheSize)
            where TKey : IEquatable<TKey>
            where TEntity : IEntity<TKey>;
        INamedEntityCache<TKey, TEntity> CreateNamedCache<TKey, TEntity>(int cacheSize)
            where TKey : IEquatable<TKey>
            where TEntity : INamedEntity<TKey>;
    }
}
