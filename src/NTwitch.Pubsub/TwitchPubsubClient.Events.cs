using System;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient
    {
        private readonly AsyncEvent<Func<Task>> _readyEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> Ready
        {
            add { _readyEvent.Add(value); }
            remove { _readyEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _streamOnlineEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> StreamOnline
        {
            add { _streamOnlineEvent.Add(value); }
            remove { _streamOnlineEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _streamOfflineEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> StreamOffline
        {
            add { _streamOfflineEvent.Add(value); }
            remove { _streamOfflineEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _whisperReceivedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> WhisperReceived
        {
            add { _whisperReceivedEvent.Add(value); }
            remove { _whisperReceivedEvent.Remove(value); }
        }
    }
}
