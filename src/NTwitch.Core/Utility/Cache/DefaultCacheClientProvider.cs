using System;

namespace NTwitch
{
    public static class DefaultCacheClientProvider
    {
        public static readonly CacheClientProvider Instance = () =>
        {
            try
            {
                return new DefaultCacheClient();
            }
            catch (PlatformNotSupportedException ex)
            {
                throw new PlatformNotSupportedException("The default CacheProvider is not supported on this platform.", ex);
            }
        };
    }
}
