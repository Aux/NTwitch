using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public partial class BaseRestClient
    {
        internal readonly AsyncEvent<Func<LogMessage, Task>> logEvent = new AsyncEvent<Func<LogMessage, Task>>();
        public event Func<LogMessage, Task> Log
        {
            add { logEvent.Add(value); }
            remove { logEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<RestTokenInfo, Task>> loggedInEvent = new AsyncEvent<Func<RestTokenInfo, Task>>();
        public event Func<RestTokenInfo, Task> LoggedIn
        {
            add { loggedInEvent.Add(value); }
            remove { loggedInEvent.Remove(value); }
        }
    }
}
