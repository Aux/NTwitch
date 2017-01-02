using System;
using System.Net.Sockets;
using System.Text;

namespace NTwitch.WebSocket
{
    public class PubsubState : IDisposable
    {
        public int BufferSize { get; set; }
        public byte[] Buffer { get; set; }
        public StringBuilder Builder { get; set; } = new StringBuilder();
        public Socket Socket = null;

        public SocketState(int buffersize = 256)
        {
            BufferSize = buffersize;
            Buffer = new byte[BufferSize];
        }

        public void Dispose()
        {
            Socket.Dispose();
        }
    }
}
