using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class PubsubApiClient
    {
        internal readonly AsyncEvent<Func<string, string, Task>> messageReceivedEvent = new AsyncEvent<Func<string, string, Task>>();
        public event Func<string, string, Task> MessageReceived
        {
            add { messageReceivedEvent.Add(value); }
            remove { messageReceivedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<long, Task>> latencyUpdatedEvent = new AsyncEvent<Func<long, Task>>();
        public event Func<long, Task> LatencyUpdated
        {
            add { latencyUpdatedEvent.Add(value); }
            remove { latencyUpdatedEvent.Remove(value); }
        }
    }
}
