using System;

namespace NTwitch.Pubsub
{
    public static class DefaultWebSocketProvider
    {
        public static readonly SocketClientProvider Instance = () =>
        {
            try
            {
                return new DefaultWebSocketClient();
            }
            catch (PlatformNotSupportedException ex)
            {
                throw new PlatformNotSupportedException("The default WebSocketProvider is not supported on this platform.", ex);
            }
        };
    }
}
