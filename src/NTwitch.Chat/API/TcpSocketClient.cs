#pragma warning disable CS1998
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal class TcpSocketClient : SocketClient, IDisposable
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private StreamWriter _writer;
        private CancellationTokenSource _cancelTokenSource;
        private Task _task;

        private int _port;
        private bool _disposed = false;

        public TcpSocketClient(LogManager logger, string host, int port)
            : base(logger, host)
        {
            _port = port;
        }

        public override async Task SendAsync(string message)
        {
            if (!_client.Connected)
                throw new InvalidOperationException("Client is not connected.");

            await _writer.WriteLineAsync(message).ConfigureAwait(false);
        }

        public override async Task ConnectAsync()
        {
            _client = new TcpClient();
            await _client.ConnectAsync(Host, _port);

            _stream = _client.GetStream();
            _writer = new StreamWriter(_stream)
            {
                AutoFlush = true,
                NewLine = "\r\n"
            };

            _cancelTokenSource = new CancellationTokenSource();
            await StartAsync(_cancelTokenSource).ConfigureAwait(false);
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
                var buffer = new byte[_client.ReceiveBufferSize];
                var receiveTask = _stream.ReadAsync(buffer, 0, _client.ReceiveBufferSize);

                var task = await Task.WhenAny(closeTask, receiveTask);
                if (task == closeTask)
                    break;

                var result = receiveTask.Result;
                string msg = Encoding.UTF8.GetString(buffer, 0, result);
                await messageReceivedEvent.InvokeAsync(msg).ConfigureAwait(false);
            }
        }

        public override async Task DisconnectAsync(bool disposing = false)
        {
            try { _cancelTokenSource.Cancel(false); } catch { }

            if (!disposing)
                await (_task ?? Task.Delay(0));

            if (_client != null && _client.Connected)
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
                    DisconnectAsync(true).GetAwaiter().GetResult();
                }
                
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
