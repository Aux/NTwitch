using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal sealed class DefaultTcpSocketClient : ISocketClient, IDisposable
    {
        public const int Port = 6667; 

        public event Func<string, Task> TextMessage;
        public event Func<Exception, Task> Closed;

        private readonly SemaphoreSlim _lock;
        private TcpClient _client;
        private NetworkStream _stream;
        private StreamWriter _writer;
        private Task _task;
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _cancelToken, _parentToken;
        private bool _disposed, _disconnecting;

        public DefaultTcpSocketClient()
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
        
        public async Task SendAsync(string message)
        {
            await _writer.WriteLineAsync(message).ConfigureAwait(false);
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

            _client = new TcpClient();
            await _client.ConnectAsync(host, Port);

            _stream = _client.GetStream();
            _writer = new StreamWriter(_stream)
            {
                AutoFlush = true,
                NewLine = "\r\n"
            };

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
                try
                {
#if NET45
                    _client.Close();
#else
                    _client.Dispose();
#endif
                    _stream.Dispose();
                    _writer.Dispose();
                }
                catch { }
                
                _client = null;
                _stream = null;
                _writer = null;
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
            var closeTask = Task.Delay(-1, cancelToken);

            try
            {
                string incompleteMessage = "";
                while (!cancelToken.IsCancellationRequested)
                {
                    var buffer = new byte[_client.ReceiveBufferSize];
                    var receiveTask = _stream.ReadAsync(buffer, 0, _client.ReceiveBufferSize);

                    var task = await Task.WhenAny(closeTask, receiveTask);
                    if (task == closeTask)
                        break;

                    var result = receiveTask.Result;
                    var message = Encoding.UTF8.GetString(buffer, 0, result);

                    int currentPos = 0;
                    while (currentPos < message.Length)
                    {
                        var msgBoundary = message.IndexOf("\r\n", currentPos);
                        if (msgBoundary == -1) // we received an incomplete message from Twitch
                        {
                            incompleteMessage = message.Substring(currentPos);
                            break;
                        }

                        var msg = message.Substring(currentPos, msgBoundary - currentPos);
                        currentPos = msgBoundary + 2; // \r\n is 2 chars

                        if (incompleteMessage != "")
                        {
                            await TextMessage(incompleteMessage + msg).ConfigureAwait(false);
                            incompleteMessage = "";
                        }
                        else
                        {
                            await TextMessage(msg).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                var _ = OnClosed(ex);
            }
        }
    }
}