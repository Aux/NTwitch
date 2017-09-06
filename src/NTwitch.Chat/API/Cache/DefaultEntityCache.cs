using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NTwitch.Chat
{
    internal class DefaultEntityCache<TKey, TEntity> : IEntityCache<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>
    {
        private readonly ConcurrentDictionary<TKey, TEntity> _entities;
        private readonly ConcurrentQueue<TKey> _orderedEntities;
        private readonly int _cacheSize;

        public IReadOnlyCollection<TEntity> Entities => _entities.Select(x => x.Value).ToArray();

        public DefaultEntityCache(int cacheSize)
        {
            _entities = new ConcurrentDictionary<TKey, TEntity>();
            _orderedEntities = new ConcurrentQueue<TKey>();
            _cacheSize = cacheSize;
        }

        public virtual void Add(TKey id, TEntity entity)
        {
            if (_entities.TryAdd(id, entity) && _cacheSize >= 0)
            {
                _orderedEntities.Enqueue(id);

                while (_orderedEntities.Count > _cacheSize && _orderedEntities.TryDequeue(out TKey entityId))
                {
                    if (_entities.TryRemove(entityId, out TEntity queueEntity))
                        OnDequeue(queueEntity);
                }
            }
        }
        protected virtual void OnDequeue(TEntity entity) { }

        public virtual TEntity Remove(TKey id)
        {
            if (_entities.TryRemove(id, out TEntity entity))
                return entity;
            return default(TEntity);
        }

        public virtual TEntity Get(TKey id)
        {
            if (_entities.TryGetValue(id, out TEntity entity))
                return entity;
            return default(TEntity);
        }

        public virtual TEntity GetOrAdd(TKey id, Func<TKey, TEntity> entityFactory)
        {
            return _entities.GetOrAdd(id, entityFactory);
        }
    }
}
