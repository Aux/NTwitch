using System;

namespace NTwitch.Chat
{
    public static class DefaultSocketProvider
    {
        public static readonly SocketClientProvider Instance = () => 
        {
            try
            {
                return new DefaultTcpSocketClient();                    
            }
            catch (PlatformNotSupportedException ex)
            {
                throw new PlatformNotSupportedException("The default SocketClientProvider is not supported on this platform.", ex);
            }
        };
    }
}
