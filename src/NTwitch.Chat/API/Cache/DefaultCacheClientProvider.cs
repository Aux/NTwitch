using System;

namespace NTwitch.Chat
{
    public static class DefaultCacheClientProvider
    {
        public static readonly CacheClientProvider Instance = (uint msgCacheSize) =>
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
