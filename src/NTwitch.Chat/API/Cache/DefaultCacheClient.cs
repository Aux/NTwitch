using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NTwitch.Chat
{
    internal sealed class DefaultCacheClient : ICacheClient
    {
        private readonly ConcurrentDictionary<ulong, ChatSimpleUser> _users;
        private readonly ConcurrentDictionary<ulong, ChatSimpleChannel> _channels;
        private readonly ConcurrentDictionary<string, ChatMessage> _messages;
        private readonly ConcurrentQueue<string> _orderedMessages;
        private readonly uint _msgCacheSize;

        public IReadOnlyCollection<ChatSimpleUser> Users => _users.Select(x => x.Value).ToArray();
        public IReadOnlyCollection<ChatSimpleChannel> Channels => _channels.Select(x => x.Value).ToArray();
        public IReadOnlyCollection<ChatMessage> Messages => _messages.Select(x => x.Value).ToArray();

        public DefaultCacheClient(uint msgCacheSize)
        {
            _users = new ConcurrentDictionary<ulong, ChatSimpleUser>();
            _channels = new ConcurrentDictionary<ulong, ChatSimpleChannel>();
            _messages = new ConcurrentDictionary<string, ChatMessage>();
            _orderedMessages = new ConcurrentQueue<string>();
            _msgCacheSize = msgCacheSize;
        }

        public void AddUser(ChatSimpleUser user)
        {
            _users[user.Id] = user;
        }

        public void AddChannel(ChatSimpleChannel channel)
        {
            _channels[channel.Id] = channel;
        }

        public void AddMessage(ChatMessage message)
        {
            if (_messages.TryAdd(message.Id, message))
            {
                _orderedMessages.Enqueue(message.Id);

                while (_orderedMessages.Count > _msgCacheSize && _orderedMessages.TryDequeue(out string msgId))
                    _messages.TryRemove(msgId, out ChatMessage msg);
            }
        }
        
        public ChatSimpleUser RemoveUser(ulong userId)
        {
            if (_users.TryRemove(userId, out ChatSimpleUser user))
                return user;
            return null;
        }

        public ChatSimpleChannel RemoveChannel(ulong channelId)
        {
            if (_channels.TryRemove(channelId, out ChatSimpleChannel channel))
                return channel;
            return null;
        }

        public ChatMessage RemoveMessage(string messageId)
        {
            if (_messages.TryRemove(messageId, out ChatMessage message))
                return message;
            return null;
        }

        public ChatSimpleUser GetUser(ulong userId)
        {
            if (_users.TryGetValue(userId, out ChatSimpleUser user))
                return user;
            return null;
        }
        
        public ChatSimpleChannel GetChannel(ulong channelId)
        {
            if (_channels.TryGetValue(channelId, out ChatSimpleChannel channel))
                return channel;
            return null;
        }
        
        public ChatMessage GetMessage(string messageId)
        {
            if (_messages.TryGetValue(messageId, out ChatMessage message))
                return message;
            return null;
        }
        
        public ChatSimpleUser GetOrAddUser(ulong userId, Func<ulong, ChatSimpleUser> userFactory)
        {
            return _users.GetOrAdd(userId, userFactory);
        }
        
        public ChatSimpleChannel GetOrAddChannel(ulong channelId, Func<ulong, ChatSimpleChannel> channelFactory)
        {
            return _channels.GetOrAdd(channelId, channelFactory);
        }
        
        public ChatMessage GetOrAddMessage(string messageId, Func<string, ChatMessage> messageFactory)
        {
            return _messages.GetOrAdd(messageId, messageFactory);
        }
    }
}
