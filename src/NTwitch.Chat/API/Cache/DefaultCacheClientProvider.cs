namespace NTwitch.Chat
{
    internal sealed class DefaultCacheClientProvider : ICacheClientProvider
    {
        public static ICacheClientProvider Instance = new DefaultCacheClientProvider();

        public ICacheClient<TKey, TEntity> Create<TKey, TEntity>(int cacheSize)
            => new DefaultCacheClient<TKey, TEntity>(cacheSize);
    }
}
