using System;
using NTwitch.Webhook;

namespace NTwitch.Helix.Webhook
{
    public static class DefaultHttpListenerProvider
    {
        public static readonly HttpListenerProvider Instance = () =>
        {
            try
            {
                return new DefaultHttpListener();
            }
            catch (PlatformNotSupportedException ex)
            {
                throw new PlatformNotSupportedException("The default HttpListenerProvider is not supported on this platform.", ex);
            }
        };
    }
}
