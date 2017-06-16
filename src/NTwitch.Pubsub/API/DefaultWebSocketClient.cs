using System;
using System.ComponentModel;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    internal sealed class DefaultWebSocketClient : IWebSocketClient, IDisposable
    {
        public const int ReceiveChunkSize = 16 * 1024; //16KB
        public const int SendChunkSize = 4 * 1024; //4KB
        private const int HR_TIMEOUT = -2147012894;

        public event Func<string, Task> TextMessage;
        public event Func<Exception, Task> Closed;

        private readonly SemaphoreSlim _lock;
        private ClientWebSocket _client;
        private Task _task;
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _cancelToken, _parentToken;
        private bool _disposed, _disconnecting;

        public DefaultWebSocketClient()
        {
            _lock = new SemaphoreSlim(1, 1);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelToken = CancellationToken.None;
            _parentToken = CancellationToken.None;
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    DisconnectInternalAsync(true).GetAwaiter().GetResult();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void SetCancelToken(CancellationToken cancelToken)
        {
            _parentToken = cancelToken;
            _cancelToken = CancellationTokenSource.CreateLinkedTokenSource(_parentToken, _cancelTokenSource.Token).Token;
        }

        public Task SendAsync(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            return SendAsync(bytes, 0, bytes.Length, true);
        }

        public async Task SendAsync(byte[] data, int index, int count, bool isText)
        {
            await _lock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (_client == null) return;

                int frameCount = (int)Math.Ceiling((double)count / SendChunkSize);

                for (int i = 0; i < frameCount; i++, index += SendChunkSize)
                {
                    bool isLast = i == (frameCount - 1);

                    int frameSize;
                    if (isLast)
                        frameSize = count - (i * SendChunkSize);
                    else
                        frameSize = SendChunkSize;

                    var type = isText ? WebSocketMessageType.Text : WebSocketMessageType.Binary;
                    await _client.SendAsync(new ArraySegment<byte>(data, index, count), type, isLast, _cancelToken).ConfigureAwait(false);
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task ConnectAsync(string host)
        {
            await _lock.WaitAsync().ConfigureAwait(false);
            try
            {
                await ConnectInternalAsync(host).ConfigureAwait(false);
            }
            finally
            {
                _lock.Release();
            }
        }

        private async Task ConnectInternalAsync(string host)
        {
            await DisconnectInternalAsync().ConfigureAwait(false);

            _cancelTokenSource = new CancellationTokenSource();
            _cancelToken = CancellationTokenSource.CreateLinkedTokenSource(_parentToken, _cancelTokenSource.Token).Token;

            _client = new ClientWebSocket();
            _client.Options.Proxy = null;
            _client.Options.KeepAliveInterval = TimeSpan.Zero;

            await _client.ConnectAsync(new Uri(host), _cancelToken).ConfigureAwait(false);
            _task = RunAsync(_cancelToken);
        }

        public async Task DisconnectAsync(bool disposing = false)
        {
            await _lock.WaitAsync().ConfigureAwait(false);
            try
            {
                await DisconnectInternalAsync().ConfigureAwait(false);
            }
            finally
            {
                _lock.Release();
            }
        }

        private async Task DisconnectInternalAsync(bool disposing = false)
        {
            try { _cancelTokenSource.Cancel(false); } catch { }

            _disconnecting = true;
            try
            {
                await (_task ?? Task.Delay(0)).ConfigureAwait(false);
                _task = null;
            }
            finally { _disconnecting = false; }

            if (_client != null)
            {
                if (!_disposed)
                {
                    try { await _client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", new CancellationToken()); }
                    catch { }
                }
                try { _client.Dispose(); }
                catch { }

                _client = null;
            }
        }

        private async Task OnClosed(Exception ex)
        {
            if (_disconnecting)
                return;

            await _lock.WaitAsync().ConfigureAwait(false);
            try
            {
                await DisconnectInternalAsync(false);
            }
            finally
            {
                _lock.Release();
            }
            await Closed(ex);
        }

        private async Task RunAsync(CancellationToken cancelToken)
        {
            var buffer = new ArraySegment<byte>(new byte[ReceiveChunkSize]);

            try
            {
                while (!cancelToken.IsCancellationRequested)
                {
                    WebSocketReceiveResult socketResult = await _client.ReceiveAsync(buffer, cancelToken).ConfigureAwait(false);
                    byte[] result;
                    int resultCount;

                    if (socketResult.MessageType == WebSocketMessageType.Close)
                        throw new WebSocketClosedException((int)socketResult.CloseStatus, socketResult.CloseStatusDescription);

                    if (!socketResult.EndOfMessage)
                    {
                        using (var stream = new MemoryStream())
                        {
                            stream.Write(buffer.Array, 0, socketResult.Count);
                            do
                            {
                                if (cancelToken.IsCancellationRequested) return;
                                socketResult = await _client.ReceiveAsync(buffer, cancelToken).ConfigureAwait(false);
                                stream.Write(buffer.Array, 0, socketResult.Count);
                            }
                            while (socketResult == null || !socketResult.EndOfMessage);
                            
                            resultCount = (int)stream.Length;
                            if (stream.TryGetBuffer(out var streamBuffer))
                                result = streamBuffer.Array;
                            else
                                result = stream.ToArray();
                        }
                    }
                    else
                    {
                        resultCount = socketResult.Count;
                        result = buffer.Array;
                    }

                    if (socketResult.MessageType == WebSocketMessageType.Text)
                    {
                        string text = Encoding.UTF8.GetString(result, 0, resultCount);
                        await TextMessage(text).ConfigureAwait(false);
                    }
                }
            }
            catch (Win32Exception ex) when (ex.HResult == HR_TIMEOUT)
            {
                var _ = OnClosed(new Exception("Connection timed out.", ex));
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                var _ = OnClosed(ex);
            }
        }
    }
}