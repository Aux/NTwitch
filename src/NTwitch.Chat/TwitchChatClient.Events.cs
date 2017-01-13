using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient
    {
        private readonly AsyncEvent<Func<string, Task>> _messageReceivedEvent = new AsyncEvent<Func<string, Task>>();
        public event Func<string, Task> MessageReceived
        {
            add { _messageReceivedEvent.Add(value); }
            remove { _messageReceivedEvent.Remove(value); }
        }
    }
}
