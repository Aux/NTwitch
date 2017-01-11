using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class SocketApiClient : IDisposable
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
        private bool _disposed;

        private readonly AsyncEvent<Func<LogMessage, Task>> _messageReceivedEvent = new AsyncEvent<Func<LogMessage, Task>>();
        public event Func<LogMessage, Task> MessageReceived
        {
            add { _messageReceivedEvent.Add(value); }
            remove { _messageReceivedEvent.Remove(value); }
        }

        public SocketApiClient(LogManager manager, string host, int port = 11000)
        {
            _log = manager;
            _host = host;
            _port = port;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }
}