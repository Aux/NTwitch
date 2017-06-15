using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NTwitch
{
    internal sealed class DefaultCacheClient : ICacheClient
    {
        private readonly ConcurrentDictionary<ulong, IUser> _users;
        private readonly ConcurrentDictionary<ulong, IChannel> _channels;
        private readonly ConcurrentDictionary<string, IMessage> _messages;
        private readonly int _msgCacheSize;

        public IReadOnlyCollection<IUser> Users => _users.Select(x => x.Value).ToArray();
        public IReadOnlyCollection<IChannel> Channels => _channels.Select(x => x.Value).ToArray();
        public IReadOnlyCollection<IMessage> Messages => _messages.Select(x => x.Value).ToArray();

        public DefaultCacheClient(int msgCacheSize)
        {
            _users = new ConcurrentDictionary<ulong, IUser>();
            _channels = new ConcurrentDictionary<ulong, IChannel>();
            _messages = new ConcurrentDictionary<string, IMessage>();
            _msgCacheSize = msgCacheSize;
        }

        public void AddUser(IUser user)
        {
            _users[user.Id] = user;
        }

        public void AddChannel(IChannel channel)
        {
            _channels[channel.Id] = channel;
        }

        public void AddMessage(IMessage message)
        {
            _messages[message.Id] = message;
        }
        
        public IUser RemoveUser(ulong userId)
        {
            if (_users.TryRemove(userId, out IUser user))
                return user;
            return null;
        }

        public IChannel RemoveChannel(ulong channelId)
        {
            if (_channels.TryRemove(channelId, out IChannel channel))
                return channel;
            return null;
        }

        public IMessage RemoveMessage(string messageId)
        {
            if (_messages.TryRemove(messageId, out IMessage message))
                return message;
            return null;
        }

        public IUser GetUser(ulong userId)
        {
            if (_users.TryGetValue(userId, out IUser user))
                return user;
            return null;
        }
        
        public IChannel GetChannel(ulong channelId)
        {
            if (_channels.TryGetValue(channelId, out IChannel channel))
                return channel;
            return null;
        }
        
        public IMessage GetMessage(string messageId)
        {
            if (_messages.TryGetValue(messageId, out IMessage message))
                return message;
            return null;
        }
        
        public IUser GetOrAddUser(ulong userId, Func<ulong, IUser> userFactory)
        {
            return _users.GetOrAdd(userId, userFactory);
        }
        
        public IChannel GetOrAddChannel(ulong channelId, Func<ulong, IChannel> channelFactory)
        {
            return _channels.GetOrAdd(channelId, channelFactory);
        }
        
        public IMessage GetOrAddMessage(string messageId, Func<string, IMessage> messageFactory)
        {
            return _messages.GetOrAdd(messageId, messageFactory);
        }
    }
}
