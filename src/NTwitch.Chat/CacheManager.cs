using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NTwitch.Chat
{
    internal class CacheManager
    {
        private readonly INamedEntityCache<ulong, ChatSimpleUser> _users;
        private readonly INamedEntityCache<ulong, ChatSimpleChannel> _channels;
        private readonly IEntityCache<string, ChatMessage> _messages;

        private readonly ConcurrentDictionary<string, List<string>> _names;
        private readonly ConcurrentDictionary<string, API.UserStateEvent> _userStates;
        
        public IReadOnlyCollection<ChatSimpleUser> Users => _users.Entities;
        public IReadOnlyCollection<ChatSimpleChannel> Channels => _channels.Entities;
        public IReadOnlyCollection<ChatMessage> Messages => _messages.Entities;

        public CacheManager(int msgCacheSize, IEntityCacheProvider cacheProvider)
        {
            _users = cacheProvider.CreateNamedCache<ulong, ChatSimpleUser>(-1);
            _channels = cacheProvider.CreateNamedCache<ulong, ChatSimpleChannel>(-1);
            _messages = cacheProvider.CreateCache<string, ChatMessage>(msgCacheSize);
            _names = new ConcurrentDictionary<string, List<string>>();
            _userStates = new ConcurrentDictionary<string, API.UserStateEvent>();
        }

        public void AddUser(ChatSimpleUser user)
            => _users.Add(user.Id, user);
        public void AddChannel(ChatSimpleChannel channel)
            => _channels.Add(channel.Id, channel);
        public void AddMessage(ChatMessage message)
            => _messages.Add(message.Id, message);

        public ChatSimpleUser RemoveUser(ulong userId)
            => _users.Remove(userId);
        public ChatSimpleUser RemoveUser(string name)
            => _users.Remove(name);
        public ChatSimpleChannel RemoveChannel(ulong channelId)
            => _channels.Remove(channelId);
        public ChatSimpleChannel RemoveChannel(string name)
            => _channels.Remove(name);
        public ChatMessage RemoveMessage(string messageId)
            => _messages.Remove(messageId);

        public ChatSimpleUser GetUser(ulong userId)
            => _users.Get(userId);
        public ChatSimpleUser GetUser(string name)
            => _users.Get(name);
        public ChatSimpleChannel GetChannel(ulong channelId)
            => _channels.Get(channelId);
        public ChatSimpleChannel GetChannel(string name)
            => _channels.Get(name);
        public ChatMessage GetMessage(string messageId)
            => _messages.Get(messageId);
        
        public ChatSimpleUser GetOrAddUser(ulong userId, Func<ulong, ChatSimpleUser> userFactory)
            => _users.GetOrAdd(userId, userFactory);
        public ChatSimpleChannel GetOrAddChannel(ulong channelId, Func<ulong, ChatSimpleChannel> channelFactory)
            => _channels.GetOrAdd(channelId, channelFactory);
        public ChatMessage GetOrAddMessage(string messageId, Func<string, ChatMessage> messageFactory)
            => _messages.GetOrAdd(messageId, messageFactory);

        // pls fix

        public void AddNames(string channelName, string[] names)
        {
            if (_names.TryGetValue(channelName, out List<string> existing))
            {
                existing.AddRange(names);
                _names.AddOrUpdate(channelName, existing, (n, l) => l);
            }
            else
            {
                _names.TryAdd(channelName, new List<string>(names));
            }
        }
        public List<string> RemoveNames(string channelName)
        {
            if (_names.TryRemove(channelName, out List<string> value))
                return value;
            return null;
        }
        public List<string> GetNames(string channelName)
        {
            if (_names.TryGetValue(channelName, out List<string> value))
                return value;
            return null;
        }

        public void AddUserState(API.UserStateEvent userState)
        {
            _userStates.TryAdd(userState.ChannelName, userState);
        }
        public API.UserStateEvent RemoveUserState(string channelName)
        {
            if (_userStates.TryRemove(channelName, out API.UserStateEvent value))
                return value;
            return null;
        }
        public API.UserStateEvent GetUserState(string channelName)
        {
            if (_userStates.TryGetValue(channelName, out API.UserStateEvent value))
                return value;
            return null;
        }
    }
}
