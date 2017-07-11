using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient
    {
        //General
        public event Func<Task> Connected { add { _connectedEvent.Add(value); } remove { _connectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _connectedEvent = new AsyncEvent<Func<Task>>();

        public event Func<Exception, Task> Disconnected { add { _disconnectedEvent.Add(value); } remove { _disconnectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Exception, Task>> _disconnectedEvent = new AsyncEvent<Func<Exception, Task>>();

        public event Func<int, int, Task> LatencyUpdated { add { _latencyUpdatedEvent.Add(value); } remove { _latencyUpdatedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<int, int, Task>> _latencyUpdatedEvent = new AsyncEvent<Func<int, int, Task>>();

        // Anonymous
        public event Func<string, Task> AnonymousReceived { add { _anonymousReceivedEvent.Add(value); } remove { _anonymousReceivedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _anonymousReceivedEvent = new AsyncEvent<Func<string, Task>>();

        // Channels
        public event Func<PubsubBitsMessage, Task> BitsReceived { add { _bitsReceivedEvent.Add(value); } remove { _bitsReceivedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<PubsubBitsMessage, Task>> _bitsReceivedEvent = new AsyncEvent<Func<PubsubBitsMessage, Task>>();

        public event Func<PubsubCommerceEvent, Task> CommerceReceived { add { _commerceReceieved.Add(value); } remove { _commerceReceieved.Remove(value); } }
        private readonly AsyncEvent<Func<PubsubCommerceEvent, Task>> _commerceReceieved = new AsyncEvent<Func<PubsubCommerceEvent, Task>>();

        public event Func<PubsubSubscriptionEvent, Task> SubscriptionReceived { add { _subscriptionReceived.Add(value); } remove { _subscriptionReceived.Remove(value); } }
        private readonly AsyncEvent<Func<PubsubSubscriptionEvent, Task>> _subscriptionReceived = new AsyncEvent<Func<PubsubSubscriptionEvent, Task>>();
    }
}
