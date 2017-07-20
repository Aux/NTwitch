using System;

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
        
        internal Cacheable(TEntity value, TKey key, bool hasValue)
        {
            Value = value;
            Key = key;
            HasValue = hasValue;
        }
    }
}
