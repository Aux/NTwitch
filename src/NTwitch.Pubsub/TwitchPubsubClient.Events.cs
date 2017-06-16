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

        // Anonymous
        public event Func<string, Task> AnonymousReceived { add { _anonymousReceivedEvent.Add(value); } remove { _anonymousReceivedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _anonymousReceivedEvent = new AsyncEvent<Func<string, Task>>();

        // Channels
        public event Func<PubsubSubscription, Task> SubscriptionReceived { add { _subscriptionReceived.Add(value); } remove { _subscriptionReceived.Remove(value); } }
        private readonly AsyncEvent<Func<PubsubSubscription, Task>> _subscriptionReceived = new AsyncEvent<Func<PubsubSubscription, Task>>();

        public event Func<string, Task> BitsReceived { add { _bitsReceivedEvent.Add(value); } remove { _bitsReceivedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _bitsReceivedEvent = new AsyncEvent<Func<string, Task>>();

        // Messages
        public event Func<string, Task> WhisperReceived { add { _whisperReceivedEvent.Add(value); } remove { _whisperReceivedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _whisperReceivedEvent = new AsyncEvent<Func<string, Task>>();
    }
}
