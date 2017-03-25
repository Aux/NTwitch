using System;
using System.Threading.Tasks;

namespace NTwitch.Tcp
{
    public partial class TwitchTcpClient
    {
        internal readonly AsyncEvent<Func<Task>> connectedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> Connected
        {
            add { connectedEvent.Add(value); }
            remove { connectedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> disconnectedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> Disconnected
        {
            add { disconnectedEvent.Add(value); }
            remove { disconnectedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> readyEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> Ready
        {
            add { readyEvent.Add(value); }
            remove { readyEvent.Remove(value); }
        }
    }
}
