using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class SocketApiClient : IDisposable
    {
        private LogManager _log;
        private SocketState _state;
        private string _baseurl;
        private string _token;
        private int _port;

        public SocketApiClient(LogManager manager, string baseurl, int port = 11000)
        {
            _log = manager;
            _baseurl = baseurl;
            _port = port;
        }

        private readonly AsyncEvent<Func<LogMessage, Task>> _messageReceivedEvent = new AsyncEvent<Func<LogMessage, Task>>();
        public event Func<LogMessage, Task> MessageReceived
        {
            add { _messageReceivedEvent.Add(value); }
            remove { _messageReceivedEvent.Remove(value); }
        }

        public async Task SendAsync(string json)
        {

        }

        public Task SendHeartbeatAsync()
        {
            throw new NotImplementedException();
        }

        public Task ReconnectAsync()
        {
            throw new NotImplementedException();
        }

        public async Task LoginAsync()
        {
            await _state.ConnectAsync(_baseurl, _port);
        }

        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _state.Dispose();
        }
    }
}