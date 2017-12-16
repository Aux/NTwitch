using System;
using System.Threading.Tasks;

namespace NTwitch.Helix
{
    public struct Cacheable<TEntity, TId>
        where TEntity : IEntity<TId>
        where TId : IEquatable<TId>
    {
        public bool HasValue { get; }
        public TId Id { get; }
        public TEntity Value { get; }
        private Func<Task<TEntity>> DownloadFunc { get; }

        internal Cacheable(TEntity value, TId id, bool hasValue, Func<Task<TEntity>> downloadFunc)
        {
            Value = value;
            Id = id;
            HasValue = hasValue;
            DownloadFunc = downloadFunc;
        }

        public async Task<TEntity> DownloadAsync()
        {
            return await DownloadFunc();
        }

        public async Task<TEntity> GetOrDownloadAsync() => HasValue ? Value : await DownloadAsync();
    }
}