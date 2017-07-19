using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NTwitch.Chat
{
    internal sealed class DefaultCacheClient<TKey, TEntity> : ICacheClient<TKey, TEntity>
    {
        private readonly ConcurrentDictionary<TKey, TEntity> _entities;
        private readonly ConcurrentQueue<TKey> _orderedEntities;
        private readonly int _cacheSize;

        public IReadOnlyCollection<TEntity> Entities => _entities.Select(x => x.Value).ToArray();

        public DefaultCacheClient(int cacheSize)
        {
            _entities = new ConcurrentDictionary<TKey, TEntity>();
            _orderedEntities = new ConcurrentQueue<TKey>();
            _cacheSize = cacheSize;
        }
        
        public void Add(TKey id, TEntity entity)
        {
            if (_entities.TryAdd(id, entity) && _cacheSize >= 0)
            {
                _orderedEntities.Enqueue(id);

                while (_orderedEntities.Count > _cacheSize && _orderedEntities.TryDequeue(out TKey entityId))
                    _entities.TryRemove(entityId, out TEntity msg);
            }
        }
        
        public TEntity Remove(TKey id)
        {
            if (_entities.TryRemove(id, out TEntity entity))
                return entity;
            return default(TEntity);
        }
        
        public TEntity Get(TKey id)
        {
            if (_entities.TryGetValue(id, out TEntity entity))
                return entity;
            return default(TEntity);
        }
        
        public TEntity GetOrAdd(TKey id, Func<TKey, TEntity> entityFactory)
        {
            return _entities.GetOrAdd(id, entityFactory);
        }
    }
}
