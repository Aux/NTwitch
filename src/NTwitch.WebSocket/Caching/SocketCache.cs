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
    }
}
