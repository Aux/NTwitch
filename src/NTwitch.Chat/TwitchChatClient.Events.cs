using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient
    {
        private readonly AsyncEvent<Func<Task>> _readyEvent = new AsyncEvent<Func<ChatChannel, Task>>();
        public event Func<Task> Ready
        {
            add { _readyEvent.Add(value); }
            remove { _readyEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<string, Task>> _messageReceivedEvent = new AsyncEvent<Func<string, ChatChannel, Task>>();
        public event Func<string, Task> MessageReceived
        {
            add { _messageReceivedEvent.Add(value); }
            remove { _messageReceivedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _channelModeChangedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> ChannelModeChanged
        {
            add { _channelModeChangedEvent.Add(value); }
            remove { _channelModeChangedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _userModeChanged = new AsyncEvent<Func<Task>>();
        public event Func<Task> UserModeChanged
        {
            add { _userModeChanged.Add(value); }
            remove { _userModeChanged.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _hostStartedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> HostingStarted
        {
            add { _hostStartedEvent.Add(value); }
            remove { _hostStartedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _hostEndedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> HostingEnded
        {
            add { _hostEndedEvent.Add(value); }
            remove { _hostEndedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _userJoinedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> UserJoined
        {
            add { _userJoinedEvent.Add(value); }
            remove { _userJoinedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _userLeftEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> UserLeft
        {
            add { _userLeftEvent.Add(value); }
            remove { _userLeftEvent.Remove(value); }
        }
    }
}
