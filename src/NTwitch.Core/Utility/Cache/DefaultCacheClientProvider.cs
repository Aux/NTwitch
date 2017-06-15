using System;

namespace NTwitch
{
    public static class DefaultCacheClientProvider
    {
        public static readonly CacheClientProvider Instance = (int msgCacheSize) =>
        {
            try
            {
                return new DefaultCacheClient(msgCacheSize);
            }
            catch (PlatformNotSupportedException ex)
            {
                throw new PlatformNotSupportedException("The default CacheProvider is not supported on this platform.", ex);
            }
        };
    }
}
