using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class SocketState : IDisposable
    {
        public int BufferSize { get; set; }
        public byte[] Buffer { get; set; }
        public StringBuilder Builder { get; set; } = new StringBuilder();
        public Socket Socket
            => _client;

        private Socket _client;

        public SocketState(int buffersize = 256)
        {
            BufferSize = buffersize;
            Buffer = new byte[BufferSize];
        }

        public async Task ConnectAsync(string url, int port)
        {
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await _client.ConnectAsync(url, port);
        }

        public Task SendAsync(string json)
        {
            throw new NotImplementedException();
        }
        
        public void Dispose()
        {
            Socket.Dispose();
        }
    }
}
