using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Chat.API
{
    public class ChatTcpClient : IDisposable
    {
        private LogManager _log;
        private MessageParser _parser;
        private TcpClient _tcp;
        private NetworkStream _stream;
        private StreamWriter _writer;
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _cancelToken;
        private Task _task;
        private string _host;
        private int _port;
        private string _token;
        private string _username;
        private bool _disposed;
        private bool _isdisposing;

        public ChatTcpClient(LogManager log, string url, int port)
        {
            _log = log;
            _host = url;
            _port = port;
        }
        
        public async Task ConnectAsync()
        {
            _tcp = new TcpClient();
            await _tcp.Client.ConnectAsync(_host, _port);

            _stream = _tcp.GetStream();
            _writer = new StreamWriter(_stream)
            {
                AutoFlush = true,
                NewLine = "\r\n"
            };

            await StartAsync(_cancelToken);
        }

        public async Task LoginAsync(string username, string token)
        {
            if (!_tcp.Client.Connected)
                throw new InvalidOperationException("You must connect before logging in.");

            _token = token;
            _username = username;

            await SendAsync("PASS " + token);
            await SendAsync("NICK " + username);
        }
        
        public async Task SendAsync(string message)
        {
            await _log.InfoAsync("Chat", "<- " + message);
            await _writer.WriteLineAsync(message);
        }

        public async Task StartAsync(CancellationToken cancelToken)
        {
            _parser = new MessageParser(_log, this);
            _task = RunAsync(cancelToken);
        }

        public async Task RunAsync(CancellationToken cancelToken)
        {
            var closeTask = Task.Delay(-1, cancelToken);
            while (!cancelToken.IsCancellationRequested)
            {
                var data = new byte[_tcp.ReceiveBufferSize];
                var receiveTask = _stream.ReadAsync(data, 0, _tcp.ReceiveBufferSize);
                var task = await Task.WhenAny(closeTask, receiveTask).ConfigureAwait(false);
                if (task == closeTask)
                    break;

                var result = receiveTask.Result;
                string message = Encoding.ASCII.GetString(data, 0, result);
                await _parser.Parse(message);
            }
        }

        public async Task StopAsync()
        {
            try { _cancelTokenSource.Cancel(false); } catch { }

            if (!_isdisposing)
                await (_task ?? Task.Delay(0)).ConfigureAwait(false);

            if (_tcp != null)
            {
                try { _tcp.Client.Shutdown(new SocketShutdown()); } catch { }
                _tcp = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    StopAsync().GetAwaiter().GetResult();
                _disposed = true;
            }
        }
    }
}
