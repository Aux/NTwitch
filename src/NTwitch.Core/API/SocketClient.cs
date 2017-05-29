using System;
using System.Threading.Tasks;

namespace NTwitch
{
    public abstract class SocketClient : ISocketClient
    {
        public LogManager Logger { get; }
        public string Host { get; }

        public SocketClient(LogManager logger, string host)
        {
            Logger = logger;
            Host = host;
        }
        
        public abstract Task ConnectAsync();
        public abstract Task DisconnectAsync(bool disposing = false);
        public abstract Task SendAsync(string message);

        public readonly AsyncEvent<Func<Task>> connectedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> Connected
        {
            add { connectedEvent.Add(value); }
            remove { connectedEvent.Remove(value); }
        }

        public readonly AsyncEvent<Func<Task>> disconnectedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> Disconnected
        {
            add { disconnectedEvent.Add(value); }
            remove { disconnectedEvent.Remove(value); }
        }

        public readonly AsyncEvent<Func<string, Task>> messageReceivedEvent = new AsyncEvent<Func<string, Task>>();
        public event Func<string, Task> MessageReceived
        {
            add { messageReceivedEvent.Add(value); }
            remove { messageReceivedEvent.Remove(value); }
        }
    }
}
