using System;

namespace NTwitch.Chat
{
    public sealed class DefaultEntityCacheProvider : IEntityCacheProvider
    {
        public static IEntityCacheProvider Instance = new DefaultEntityCacheProvider();

        public IEntityCache<TKey, TEntity> CreateCache<TKey, TEntity>(int cacheSize)
            where TKey : IEquatable<TKey>
            where TEntity : IEntity<TKey>
        {
            return new DefaultEntityCache<TKey, TEntity>(cacheSize);
        }

        public INamedEntityCache<TKey, TEntity> CreateNamedCache<TKey, TEntity>(int cacheSize)
            where TKey : IEquatable<TKey>
            where TEntity : INamedEntity<TKey>
        {
            return new DefaultNamedEntityCache<TKey, TEntity>(cacheSize);
        }
    }
}
