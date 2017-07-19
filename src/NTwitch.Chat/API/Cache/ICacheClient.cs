using System;
using System.Collections.Generic;

namespace NTwitch.Chat
{
    public interface ICacheClient<TKey, TEntity>
    {
        IReadOnlyCollection<TEntity> Entities { get; }
        
        void Add(TKey id, TEntity entity);

        TEntity Remove(TKey id);
        TEntity Get(TKey id);
        TEntity GetOrAdd(TKey id, Func<TKey, TEntity> factory);
    }
}
