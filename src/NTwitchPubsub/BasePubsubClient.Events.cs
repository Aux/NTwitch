using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class BasePubsubClient
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
    }
}
