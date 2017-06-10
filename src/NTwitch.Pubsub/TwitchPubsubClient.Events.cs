using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient
    {
        //General
        public event Func<Task> Connected { add { _connectedEvent.Add(value); } remove { _connectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _connectedEvent = new AsyncEvent<Func<Task>>();

        public event Func<Task> Disconnected { add { _disconnectedEvent.Add(value); } remove { _disconnectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _disconnectedEvent = new AsyncEvent<Func<Task>>();

        public event Func<int, int, Task> LatencyUpdated { add { _latencyUpdatedEvent.Add(value); } remove { _latencyUpdatedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<int, int, Task>> _latencyUpdatedEvent = new AsyncEvent<Func<int, int, Task>>();
    }
}
