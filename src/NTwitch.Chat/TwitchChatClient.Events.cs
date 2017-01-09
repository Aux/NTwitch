using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient
    {
        private readonly AsyncEvent<Func<ChatChannel, ChatUser, Task>> _userJoinedEvent = new AsyncEvent<Func<ChatChannel, ChatUser, Task>>();
        public event Func<ChatChannel, ChatUser, Task> UserJoined
        {
            add { _userJoinedEvent.Add(value); }
            remove { _userJoinedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<ChatChannel, ChatUser, Task>> _userLeftEvent = new AsyncEvent<Func<ChatChannel, ChatUser, Task>>();
        public event Func<ChatChannel, ChatUser, Task> UserLeft
        {
            add { _userLeftEvent.Add(value); }
            remove { _userLeftEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<ChatChannel, ChatUser, Task>> _userModeChangedEvent = new AsyncEvent<Func<ChatChannel, ChatUser, Task>>();
        public event Func<ChatChannel, ChatUser, Task> UserModeChanged
        {
            add { _userModeChangedEvent.Add(value); }
            remove { _userModeChangedEvent.Remove(value); }
        }
        
        private readonly AsyncEvent<Func<ChatMessage, Task>> _messageReceived = new AsyncEvent<Func<ChatMessage, Task>>();
        public event Func<ChatMessage, Task> MessageReceived
        {
            add { _messageReceived.Add(value); }
            remove { _messageReceived.Remove(value); }
        }

        private readonly AsyncEvent<Func<ChatBan, Task>> _userBannedEvent = new AsyncEvent<Func<ChatBan, Task>>();
        public event Func<ChatBan, Task> UserBanned
        {
            add { _userBannedEvent.Add(value); }
            remove { _userBannedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Task>> _channelStateChangedEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> ChannelStateChanged
        {
            add { _channelStateChangedEvent.Add(value); }
            remove { _channelStateChangedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<ChatUser, Task>> _userStateChangedEvent = new AsyncEvent<Func<ChatUser, Task>>();
        public event Func<ChatUser, Task> UserStateChanged
        {
            add { _userStateChangedEvent.Add(value); }
            remove { _userStateChangedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<ChatChannel, ChatChannel, Task>> _hostStartedEvent = new AsyncEvent<Func<ChatChannel, ChatChannel, Task>>();
        public event Func<ChatChannel, ChatChannel, Task> HostStarted
        {
            add { _hostStartedEvent.Add(value); }
            remove { _hostStartedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<ChatChannel, ChatChannel, int, Task>> _hostEndedEvent = new AsyncEvent<Func<ChatChannel, ChatChannel, int, Task>>();
        public event Func<ChatChannel, ChatChannel, int, Task> HostEnded
        {
            add { _hostEndedEvent.Add(value); }
            remove { _hostEndedEvent.Remove(value); }
        }
    }
}
