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
        private TcpClient _tcp;
        private NetworkStream _stream;
        private StreamWriter _writer;
        private CancellationTokenSource _cancelTokenSource;
        private Task _task;
        private string _host;
        private int _port;
        private string _token;
        private string _username;
        private bool _disposed;
        private bool _isdisposing;

        private readonly AsyncEvent<Func<string, Task>> _messageReceived = new AsyncEvent<Func<string, Task>>();
        internal event Func<string, Task> MessageReceived
        {
            add { _messageReceived.Add(value); }
            remove { _messageReceived.Remove(value); }
        }

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
                NewLine = "\n"
            };

            await StartAsync(_cancelTokenSource);
        }

        public async Task LoginAsync(string username, string token)
        {
            if (!_tcp.Client.Connected)
                throw new InvalidOperationException("You must connect before logging in.");

            _token = token;
            _username = username;
            _cancelTokenSource = new CancellationTokenSource();

            await SendAsync("PASS oauth:" + token);
            await SendAsync("NICK " + username);
            await StartAsync(_cancelTokenSource);
            await SendAsync("CAP REQ :twitch.tv/tags");
        }
        
        public async Task SendAsync(string message)
        {
            await _log.InfoAsync("Chat", message);
            await _writer.WriteLineAsync(message);
        }

        private async Task StartAsync(CancellationTokenSource cancelToken)
        {
            _task = RunAsync(cancelToken);
        }
        
        private async Task RunAsync(CancellationTokenSource cancelTokenSource)
        {
            var closeTask = Task.Delay(-1, cancelTokenSource.Token);
            while (!cancelTokenSource.IsCancellationRequested)
            {
                var data = new byte[_tcp.ReceiveBufferSize];
                var receiveTask = _stream.ReadAsync(data, 0, _tcp.ReceiveBufferSize);
                var task = await Task.WhenAny(closeTask, receiveTask).ConfigureAwait(false);
                if (task == closeTask)
                    break;

                var result = receiveTask.Result;
                string message = Encoding.UTF8.GetString(data, 0, result);
                if (!string.IsNullOrWhiteSpace(message))
                    await _messageReceived.InvokeAsync(message);
            }
        }

        public void Dispose()
        {
            _tcp.Dispose();
            _tcp = null;
        }
    }
}
