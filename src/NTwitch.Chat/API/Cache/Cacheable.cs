using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public struct Cacheable<TKey, TEntity>
        where TKey : IEquatable<TKey>
    {
        /// <summary> True if this entity cached </summary>
        public bool HasValue { get; }
        /// <summary> The value used to identify this entity in the cache </summary>
        public TKey Key { get; }
        /// <summary> The entity, if it can be pulled from the cache. </summary>
        public TEntity Value { get; }
        
        private Func<Task<TEntity>> DownloadFunc { get; }

        internal Cacheable(TEntity value, TKey key, bool hasValue, Func<Task<TEntity>> downloadFunc)
        {
            Value = value;
            Key = key;
            HasValue = hasValue;
            DownloadFunc = downloadFunc;
        }

        /// <summary> Downloads a version of this entity to the cache. </summary>
        public async Task<TEntity> DownloadAsync()
        {
            return await DownloadFunc();
        }
        
        /// <summary> Returns the cached entity if it exists; otherwise downloads it </summary>
        public async Task<TEntity> GetOrDownloadAsync() => HasValue ? Value : await DownloadAsync();
    }
}
