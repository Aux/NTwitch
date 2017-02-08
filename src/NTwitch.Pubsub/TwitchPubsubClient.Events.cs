using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public partial class TwitchPubsubClient
    {
        internal readonly AsyncEvent<Func<Task>> _readyEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> Ready
        {
            add { _readyEvent.Add(value); }
            remove { _readyEvent.Remove(value); }
        }
        
        internal readonly AsyncEvent<Func<Task>> _bitsReceivedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> BitsReceived
        {
            add { _bitsReceivedEvent.Add(value); }
            remove { _bitsReceivedEvent.Remove(value); }
        }
    }
}
