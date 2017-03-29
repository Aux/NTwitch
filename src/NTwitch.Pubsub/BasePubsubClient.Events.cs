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

        internal readonly AsyncEvent<Func<Task>> subscriptionAddedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> SubscriptionAdded
        {
            add { subscriptionAddedEvent.Add(value); }
            remove { subscriptionAddedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> subscriptionRemovedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> SubscriptionRemoved
        {
            add { subscriptionRemovedEvent.Add(value); }
            remove { subscriptionRemovedEvent.Remove(value); }
        }
    }
}
