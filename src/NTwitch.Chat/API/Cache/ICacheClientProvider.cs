namespace NTwitch.Chat
{
    public interface ICacheClientProvider
    {
        ICacheClient<TKey, TEntity> Create<TKey, TEntity>(int cacheSize);
    }
}
