#pragma warning disable CS1998
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatClient : IDisposable
    {
        private TcpClient _client;
        private LogManager _log;
        private NetworkStream _stream;
        private StreamWriter _writer;
        private CancellationTokenSource _cancelTokenSource;
        private Task _task;

        private string _host;
        private int _port;
        private string _username;
        private string _token;
        private bool _disposed;

        private readonly AsyncEvent<Func<string, Task>> _messageReceivedEvent = new AsyncEvent<Func<string, Task>>();
        internal event Func<string, Task> MessageReceived
        {
            add { _messageReceivedEvent.Add(value); }
            remove { _messageReceivedEvent.Remove(value); }
        }

        internal ChatClient(LogManager log, string host, int port)
        {
            _log = log;
            _host = host;
            _port = port;
        }

        public async Task SendAsync(string message)
        {
            await _log.DebugAsync("Chat", message);
            await _writer.WriteLineAsync(message);
        }

        internal async Task ConnectAsync()
        {
            _client = new TcpClient();
            await _client.ConnectAsync(_host, _port);

            _stream = _client.GetStream();
            _writer = new StreamWriter(_stream)
            {
                AutoFlush = true,
                NewLine = "\r\n"
            };

            _cancelTokenSource = new CancellationTokenSource();
            await StartAsync(_cancelTokenSource);
        }

        internal async Task LoginAsync(string username, string token)
        {
            if (!_client.Connected)
                throw new InvalidOperationException("You must connect before logging in.");

            _token = token;
            _username = username;

            await SendAsync("PASS oauth:" + token);
            await SendAsync("NICK " + username);
            await SendAsync("CAP REQ :twitch.tv/tags");
            await SendAsync("CAP REQ :twitch.tv/membership");
        }

        public async Task DisconnectAsync(bool disposing = false)
        {
            try { _cancelTokenSource.Cancel(false); } catch { }

            if (!disposing)
                await (_task ?? Task.Delay(0)).ConfigureAwait(false);

            if (_client != null && _client.Connected)
            {
                _cancelTokenSource.Cancel(false);

                try { _client.Dispose(); } catch { }
                _client = null;
            }
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

                var task = await Task.WhenAny(closeTask, receiveTask).ConfigureAwait(false);
                if (task == closeTask)
                    break;

                var result = receiveTask.Result;
                string msg = Encoding.ASCII.GetString(buffer, 0, result);
                await _messageReceivedEvent.InvokeAsync(msg);
            }
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
