using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NTwitch.WebSocket
{
    public class SocketCache : ISocketCache
    {
        public IReadOnlyCollection<SocketChannel> Channels => _channels.Select(x => x.Value).ToList();
        public IReadOnlyCollection<SocketUser> Users => _users.Select(x => x.Value).ToList();

        private ConcurrentDictionary<ulong, SocketChannel> _channels;
        private ConcurrentDictionary<ulong, SocketUser> _users;

        public SocketCache()
        {
            _channels = new ConcurrentDictionary<ulong, SocketChannel>();
            _users = new ConcurrentDictionary<ulong, SocketUser>();
        }

        public SocketChannel GetChannel(ulong channelId)
        {
            if (_channels.TryGetValue(channelId, out SocketChannel channel))
                return channel;
            return null;
        }

        public SocketUser GetUser(ulong userId)
        {
            if (_users.TryGetValue(userId, out SocketUser user))
                return user;
            return null;
        }

        public void AddChannel(SocketChannel channel)
        {
            _channels.AddOrUpdate(channel.Id, channel, (id, c) => c);
        }

        public void AddUser(SocketUser user)
        {
            _users.AddOrUpdate(user.Id, user, (id, u) => u);
        }
        
        public SocketChannel RemoveChannel(ulong channelId)
        {
            if (_channels.TryRemove(channelId, out SocketChannel channel))
                return channel;
            return null;
        }

        public SocketUser RemoveUser(ulong userId)
        {
            if (_users.TryRemove(userId, out SocketUser user))
                return user;
            return null;
        }

        #region ICache
        IReadOnlyCollection<SocketChannel> ISocketCache.Channels
            => throw new NotImplementedException();
        IReadOnlyCollection<SocketUser> ISocketCache.Users
            => throw new NotImplementedException();
        IReadOnlyCollection<IChannel> ICache.Channels
            => throw new NotImplementedException();
        IReadOnlyCollection<IUser> ICache.Users
            => throw new NotImplementedException();

        SocketChannel ISocketCache.GetChannel(ulong channelId)
            => throw new NotImplementedException();
        SocketUser ISocketCache.GetUser(ulong userId)
            => throw new NotImplementedException();
        void ISocketCache.AddChannel(SocketChannel channel)
            => throw new NotImplementedException();
        void ISocketCache.AddUser(SocketUser user)
            => throw new NotImplementedException();
        SocketChannel ISocketCache.RemoveChannel(ulong channelId)
            => throw new NotImplementedException();
        SocketUser ISocketCache.RemoveUser(ulong userId)
            => throw new NotImplementedException();
        IChannel ICache.GetChannel(ulong channelId)
            => throw new NotImplementedException();
        IUser ICache.GetUser(ulong userId)
            => throw new NotImplementedException();
        void ICache.AddChannel(IChannel channel)
            => throw new NotImplementedException();
        void ICache.AddUser(IUser user)
            => throw new NotImplementedException();
        IChannel ICache.RemoveChannel(ulong channelId)
            => throw new NotImplementedException();
        IUser ICache.RemoveUser(ulong userId)
            => throw new NotImplementedException();
        #endregion
    }
}
