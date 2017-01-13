using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient
    {
        internal readonly AsyncEvent<Func<Task>> _readyEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> Ready
        {
            add { _readyEvent.Add(value); }
            remove { _readyEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _reconnectingEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> Reconnecting
        {
            add { _reconnectingEvent.Add(value); }
            remove { _reconnectingEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<ChatMessage, Task>> _messageReceivedEvent = new AsyncEvent<Func<ChatMessage, Task>>();
        public event Func<ChatMessage, Task> MessageReceived
        {
            add { _messageReceivedEvent.Add(value); }
            remove { _messageReceivedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _noticeReceivedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> NoticeReceived
        {
            add { _noticeReceivedEvent.Add(value); }
            remove { _noticeReceivedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _joinedChannelEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> JoinedChannel
        {
            add { _joinedChannelEvent.Add(value); }
            remove { _joinedChannelEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _leftChannelEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> LeftChannel
        {
            add { _leftChannelEvent.Add(value); }
            remove { _leftChannelEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _userJoinedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> UserJoined
        {
            add { _userJoinedEvent.Add(value); }
            remove { _userJoinedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _userLeftEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> UserLeft
        {
            add { _userLeftEvent.Add(value); }
            remove { _userLeftEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _hostStartedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> HostStarted
        {
            add { _hostStartedEvent.Add(value); }
            remove { _hostStartedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _hostEndedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> HostEnded
        {
            add { _hostEndedEvent.Add(value); }
            remove { _hostEndedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _userUpdatedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> UserUpdated
        {
            add { _userUpdatedEvent.Add(value); }
            remove { _userUpdatedEvent.Remove(value); }
        }

        internal readonly AsyncEvent<Func<Task>> _channelUpdatedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> ChannelUpdated
        {
            add { _channelUpdatedEvent.Add(value); }
            remove { _channelUpdatedEvent.Remove(value); }
        }
    }
}
