#pragma warning disable CS1998
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub.API
{
    internal partial class WebSocketClient : SocketClient, IDisposable
    {
        private const int _chunkSize = 1024;
        private ClientWebSocket _client = null;
        private CancellationTokenSource _cancelTokenSource;
        private LogManager _log;
        private Task _task;
        
        private string _host;
        private bool _disposed = false;

        public WebSocketClient(LogManager logger, string host)
            : base(logger, host) { }

        public override async Task SendAsync(string message)
        {
            if (_client.State != WebSocketState.Open)
                throw new InvalidOperationException("Client must be connected before sending data");

            var bytes = Encoding.UTF8.GetBytes(message);
            int count = (int)Math.Ceiling((double)bytes.Length / _chunkSize);

            for (int i = 0; i < count; i++)
            {
                var offset = _chunkSize * i;
                var buffer = new ArraySegment<Byte>(bytes, offset, count);

                bool isfinal = i + 1 == count;
                await _client.SendAsync(buffer, WebSocketMessageType.Text, isfinal, _cancelTokenSource.Token).ConfigureAwait(false);
            }
        }

        public override async Task ConnectAsync()
        {
            _cancelTokenSource = new CancellationTokenSource();
            _client = new ClientWebSocket();
            await _client.ConnectAsync(new Uri(_host), _cancelTokenSource.Token);
            await StartAsync(_cancelTokenSource);
            await connectedEvent.InvokeAsync().ConfigureAwait(false);
        }

        public async Task StartAsync(CancellationTokenSource cancelTokenSource)
        {
            _task = RunAsync(cancelTokenSource);
        }

        public async Task RunAsync(CancellationTokenSource cancelTokenSource)
        {
            var closeTask = Task.Delay(-1, cancelTokenSource.Token);

            while (!cancelTokenSource.IsCancellationRequested)
            {
                var buffer = new ArraySegment<byte>(new byte[_chunkSize]);

                var builder = new StringBuilder();
                WebSocketReceiveResult result;
                do
                {
                    var recieveTask = _client.ReceiveAsync(buffer, cancelTokenSource.Token);

                    var task = await Task.WhenAny(closeTask, recieveTask);
                    if (task == closeTask)
                        break;

                    result = recieveTask.Result;

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        cancelTokenSource.Cancel();
                        break;
                    }

                    string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    builder.Append(message);
                } while (!result.EndOfMessage);

                await messageReceivedEvent.InvokeAsync(builder.ToString()).ConfigureAwait(false);
            }

            await StopAsync(cancelTokenSource);
        }
        
        public async Task StopAsync(CancellationTokenSource cancelTokenSource)
        {
            await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancelTokenSource.Token);
            Dispose();
            await disconnectedEvent.InvokeAsync().ConfigureAwait(false);
        }

        public override async Task DisconnectAsync(bool disposing = false)
        {
            try { _cancelTokenSource.Cancel(false); } catch { }

            if (!disposing)
                await (_task ?? Task.Delay(0));

            if (_client != null && _client.State == WebSocketState.Open)
            {
                _cancelTokenSource.Cancel(false);

                try { _client.Dispose(); } catch { }
                _client = null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _cancelTokenSource.Cancel();
                    _client.Dispose();
                }
                
                _task = null;
                _client = null;
                _disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
