using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class PubsubClient : IDisposable
    {
        private ClientWebSocket _client;
        private LogManager _log;
        private CancellationTokenSource _cancelTokenSource;
        private Task _task;

        private string _host;
        private bool _disposed;

        public PubsubClient(LogManager log, string host)
        {
            _log = log;
            _host = host;
        }

        public Task<string> SendAsync(string method, object payload)
        {
            if (_client == null)
                throw new InvalidOperationException();

            throw new NotImplementedException();
        }

        public async Task ConnectAsync()
        {
            _cancelTokenSource = new CancellationTokenSource();

            _client = new ClientWebSocket();
            _client.Options.Proxy = null;
            _client.Options.KeepAliveInterval = TimeSpan.Zero;

            await _client.ConnectAsync(new Uri(_host), _cancelTokenSource.Token);
            _task = RunAsync(_cancelTokenSource.Token);
        }

        public async Task DisconnectAsync(bool disposing = false)
        {
            try { _cancelTokenSource.Cancel(false); } catch { }

            if (!disposing)
                await (_task ?? Task.Delay(0)).ConfigureAwait(false);

            if (_client != null && _client.State == WebSocketState.Open)
            {
                var token = new CancellationToken();
                if (!disposing)
                    try { await _client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", token); } catch { }
                
                try { _client.Dispose(); } catch { }
                _client = null;
            }
        }

        public Task RunAsync(CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    DisconnectAsync(true).GetAwaiter().GetResult();
                
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
