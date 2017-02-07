using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    internal class SocketClient : IDisposable
    {
        private ClientWebSocket _client;
        private CancellationTokenSource _cancelTokenSource;

        private string _host;
        private string _token;
        private bool _disposed = false;

        public SocketClient()
        {

        }

        public SocketClient(string host, string token)
        {
            _host = host;
            _token = token;

            _client = new ClientWebSocket();
        }
        
        public Task SendAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ConnectAsync()
        {
            _cancelTokenSource = new CancellationTokenSource();
            await _client.ConnectAsync(new Uri("wss://pubsub-edge.twitch.tv"), _cancelTokenSource.Token);
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
        
        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
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
