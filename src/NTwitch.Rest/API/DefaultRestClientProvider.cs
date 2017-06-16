using System;

namespace NTwitch.Rest.API
{
    public static class DefaultRestClientProvider
    {
        public static readonly RestClientProvider Instance = url =>
        {
            try
            {
                return new DefaultRestClient(url);
            }
            catch (PlatformNotSupportedException ex)
            {
                throw new PlatformNotSupportedException("The default RestClientProvider is not supported on this platform.", ex);
            }
        };
    }
}
