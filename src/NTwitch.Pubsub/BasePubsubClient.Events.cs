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

        internal readonly AsyncEvent<Func<string, Task>> subscriptionAddedEvent = new AsyncEvent<Func<string, Task>>();
        public event Func<string, Task> SubscriptionAdded
        {
            add { subscriptionAddedEvent.Add(value); }
            remove { subscriptionAddedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<string, Task>> subscriptionRemovedEvent = new AsyncEvent<Func<string, Task>>();
        public event Func<string, Task> SubscriptionRemoved
        {
            add { subscriptionRemovedEvent.Add(value); }
            remove { subscriptionRemovedEvent.Remove(value); }
        }
        
        internal readonly AsyncEvent<Func<PubsubWhisperMessage, Task>> whisperReceivedEvent = new AsyncEvent<Func<PubsubWhisperMessage, Task>>();
        public event Func<PubsubWhisperMessage, Task> WhisperReceived
        {
            add { whisperReceivedEvent.Add(value); }
            remove { whisperReceivedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> streamOnlineEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> StreamOnline
        {
            add { streamOnlineEvent.Add(value); }
            remove { streamOnlineEvent.Remove(value); }
        }
    }
}
