using System;
using System.Collections.Concurrent;

namespace NTwitch.Chat
{
    internal class DefaultNamedEntityCache<TKey, TEntity> : DefaultEntityCache<TKey, TEntity>, INamedEntityCache<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : INamedEntity<TKey>
    {
        private readonly ConcurrentDictionary<string, TKey> _nameMap;

        public DefaultNamedEntityCache(int cacheSize) 
            : base(cacheSize)
        {
            _nameMap = new ConcurrentDictionary<string, TKey>();
        }

        public override void Add(TKey id, TEntity entity)
        {
            base.Add(id, entity);
            _nameMap.TryAdd(entity.Name, id);
        }
        
        public TEntity Remove(string name)
        {
            if (_nameMap.TryRemove(name, out TKey key))
                return Get(key);
            return default(TEntity);
        }

        public TEntity Get(string name)
        {
            if (_nameMap.TryGetValue(name, out TKey key))
                return Get(key);
            return default(TEntity);
        }

        protected override void OnDequeue(TEntity entity)
        {
            _nameMap.TryRemove(entity.Name, out TKey key);
        }
    }
}
