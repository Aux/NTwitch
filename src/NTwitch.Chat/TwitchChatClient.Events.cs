using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public partial class TwitchChatClient
    {
        //General
        public event Func<Task> Connected { add { _connectedEvent.Add(value); } remove { _connectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _connectedEvent = new AsyncEvent<Func<Task>>();
        
        public event Func<Exception, Task> Disconnected { add { _disconnectedEvent.Add(value); } remove { _disconnectedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Exception, Task>> _disconnectedEvent = new AsyncEvent<Func<Exception, Task>>();
        
        public event Func<int, int, Task> LatencyUpdated { add { _latencyUpdatedEvent.Add(value); } remove { _latencyUpdatedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<int, int, Task>> _latencyUpdatedEvent = new AsyncEvent<Func<int, int, Task>>();

        // Channels
        public event Func<ChatMessage, Task> MessageReceived { add { _messageReceivedEvent.Add(value); } remove { _messageReceivedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<ChatMessage, Task>> _messageReceivedEvent = new AsyncEvent<Func<ChatMessage, Task>>();

        public event Func<Cacheable<string, ChatSimpleChannel>, Task> CurrentUserJoined { add { _currentUserJoinedEvent.Add(value); } remove { _currentUserJoinedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Cacheable<string, ChatSimpleChannel>, Task>> _currentUserJoinedEvent = new AsyncEvent<Func<Cacheable<string, ChatSimpleChannel>, Task>>();

        public event Func<Cacheable<string, ChatSimpleChannel>, Cacheable<string, ChatSimpleUser>, Task> UserJoined { add { _userJoinedEvent.Add(value); } remove { _userJoinedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Cacheable<string, ChatSimpleChannel>, Cacheable<string, ChatSimpleUser>, Task>> _userJoinedEvent = new AsyncEvent<Func<Cacheable<string, ChatSimpleChannel>, Cacheable<string, ChatSimpleUser>, Task>>();

        public event Func<string, Task> CurrentUserLeft { add { _currentUserLeftEvent.Add(value); } remove { _currentUserLeftEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, Task>> _currentUserLeftEvent = new AsyncEvent<Func<string, Task>>();

        public event Func<string, string, Task> UserLeft { add { _userLeftEvent.Add(value); } remove { _userLeftEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, string, Task>> _userLeftEvent = new AsyncEvent<Func<string, string, Task>>();

        // Moderation
        public event Func<string, string, Task> ModeratorAdded { add { _moderatorAddedEvent.Add(value); } remove { _moderatorAddedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, string, Task>> _moderatorAddedEvent = new AsyncEvent<Func<string, string, Task>>();

        public event Func<string, string, Task> ModeratorRemoved { add { _moderatorRemovedEvent.Add(value); } remove { _moderatorRemovedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<string, string, Task>> _moderatorRemovedEvent = new AsyncEvent<Func<string, string, Task>>();

        public event Func<ChatSimpleChannel, ChatSimpleUser, BanOptions, Task> UserBanned { add { _userBannedEvent.Add(value); } remove { _userBannedEvent.Remove(value); } }
        private readonly AsyncEvent<Func<ChatSimpleChannel, ChatSimpleUser, BanOptions, Task>> _userBannedEvent = new AsyncEvent<Func<ChatSimpleChannel, ChatSimpleUser, BanOptions, Task>>();

    }
}
