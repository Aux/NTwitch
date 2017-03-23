using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.WebSocket
{
    internal class SocketClient : IDisposable
    {
        private const int _chunkSize = 1024;
        private ClientWebSocket _client = null;
        private CancellationTokenSource _cancelTokenSource;
        private Task _task;

        private AuthMode _tokenType;
        private string _host;
        private string _token;
        private bool _disposed = false;

        public SocketClient(TwitchSocketConfig config, AuthMode type, string token)
        {
            _host = config.SocketHost;
            _tokenType = type;
            _token = token;
        }

        public async Task SendAsync(string message)
        {
            if (_client.State != WebSocketState.Open)
                throw new InvalidOperationException("Client must be connected before sending data");

            var bytes = Encoding.UTF8.GetBytes(message);
            int count = (int)Math.Ceiling((double)bytes.Length / _chunkSize);

            for (int i = 0; i < count; i++)
            {
                bool isfinal = i + 1 == count;

                var buffer = new ArraySegment<Byte>(bytes, _chunkSize * i, _chunkSize);

                await _client.SendAsync(buffer, WebSocketMessageType.Text, isfinal, _cancelTokenSource.Token);
            }

            // something about callbacks
        }

        public Task StartAsync()
        {
            throw new NotImplementedException();
        }

        public Task RunAsync()
        {
            throw new NotImplementedException();
        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }

        private void EnsureClientsExist()
        {
            if (_client == null)
            {

            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
