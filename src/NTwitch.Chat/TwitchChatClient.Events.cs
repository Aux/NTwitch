using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient
    {
        //General
        public event Func<Task> Connected { add { _connectedEvent.Add(value); } remove { _connectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _connectedEvent = new AsyncEvent<Func<Task>>();

        public event Func<Task> Disconnected { add { _disconnectedEvent.Add(value); } remove { _disconnectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _disconnectedEvent = new AsyncEvent<Func<Task>>();

        public event Func<int, int, Task> LatencyUpdated { add { _latencyUpdatedEvent.Add(value); } remove { _latencyUpdatedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<int, int, Task>> _latencyUpdatedEvent = new AsyncEvent<Func<int, int, Task>>();

        // Channels
        public event Func<ChatMessage, Task> MessageReceived { add { _messageReceivedEvent.Add(value); } remove { _messageReceivedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<ChatMessage, Task>> _messageReceivedEvent = new AsyncEvent<Func<ChatMessage, Task>>();

        public event Func<string, Task> JoinedChannel { add { _joinedChannelEvent.Add(value); } remove { _joinedChannelEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _joinedChannelEvent = new AsyncEvent<Func<string, Task>>();

        public event Func<string, Task> LeftChannel { add { _leftChannelEvent.Add(value); } remove { _leftChannelEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _leftChannelEvent = new AsyncEvent<Func<string, Task>>();

    }
}
