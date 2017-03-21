using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class BaseRestClient
    {
        internal readonly AsyncEvent<Func<LogMessage, Task>> logReceivedEvent = new AsyncEvent<Func<LogMessage, Task>>();
        public event Func<LogMessage, Task> Log
        {
            add { logReceivedEvent.Add(value); }
            remove { logReceivedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> loggedInEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> LoggedIn
        {
            add { loggedInEvent.Add(value); }
            remove { loggedInEvent.Remove(value); }
        }
    }
}
