#pragma warning disable CS1998
using System;
using System.ComponentModel;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub.API
{
    internal partial class WebSocketClient : SocketClient, IDisposable
    {
        private const int _chunkSize = 1024;
        private const int _timeout = -2147012894;

        private ClientWebSocket _client = null;
        private CancellationTokenSource _cancelTokenSource;
        private Task _task;
        
        private bool _disposed = false;

        public WebSocketClient(LogManager logger, string host)
            : base(logger, host) { }

        public override async Task SendAsync(string message)
        {
            if (_client == null) return;

            try
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                int count = (int)Math.Ceiling((double)bytes.Length / _chunkSize);

                for (int i = 0; i < count; i += _chunkSize)
                {
                    bool isfinal = i == (count - 1);
                    int index = _chunkSize * i;
                    var buffer = new ArraySegment<Byte>(bytes, index, count);

                    int size;
                    if (isfinal)
                        size = count - (i * _chunkSize);
                    else
                        size = _chunkSize;

                    await _client.SendAsync(buffer, WebSocketMessageType.Text, isfinal, _cancelTokenSource.Token).ConfigureAwait(false);
                }
            } catch (Exception ex)
            {
                await Logger.DebugAsync("WebSocket", ex);
            }
        }

        public override async Task ConnectAsync()
        {
            _cancelTokenSource = new CancellationTokenSource();
            _client = new ClientWebSocket();
            _client.Options.KeepAliveInterval = TimeSpan.Zero;
            
            await _client.ConnectAsync(new Uri(Host), _cancelTokenSource.Token);
            _task = RunAsync(_cancelTokenSource);
        }
        
        private async Task RunAsync(CancellationTokenSource cancelTokenSource)
        {
            var closeTask = Task.Delay(-1, cancelTokenSource.Token);

            try
            {
                while (!cancelTokenSource.IsCancellationRequested)
                {
                    var buffer = new ArraySegment<byte>(new byte[_chunkSize]);
                    var receiveTask = ReceiveAsync(buffer);

                    var task = await Task.WhenAny(closeTask, receiveTask);
                    if (task == closeTask)
                        break;

                    var result = receiveTask.Result;

                    if (result.Item1 == WebSocketMessageType.Close)
                    {
                        await StopAsync("Connect was closed by server");
                        break;
                    }

                    await messageReceivedEvent.InvokeAsync(result.Item2).ConfigureAwait(false);
                }
            }
            catch (Win32Exception ex) when (ex.HResult == _timeout)
            {
                var _ = StopAsync("Connected timed out", ex);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                var _ = StopAsync(ex.Message, ex);
            }
        }
        
        private async Task<Tuple<WebSocketMessageType, string>> ReceiveAsync(ArraySegment<byte> buffer)
        {
            var builder = new StringBuilder();
            WebSocketReceiveResult result;
            do
            {
                result = await _client.ReceiveAsync(buffer, _cancelTokenSource.Token);
                
                if (result.MessageType == WebSocketMessageType.Close)
                    break;
                
                string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                builder.Append(message);
            } while (!result.EndOfMessage);

            return Tuple.Create(result.MessageType, builder.ToString());
        }

        private async Task StopAsync(string message, Exception ex = null)
        {
            await Logger.InfoAsync(message, ex).ConfigureAwait(false);
            await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, null, _cancelTokenSource.Token);
            Dispose();
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
