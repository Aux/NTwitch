using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NTwitch.Rest
{
    public class RestCache : ICache
    {
        public IReadOnlyCollection<IChannel> Channels => _channels.Select(x => x.Value).ToArray();
        public IReadOnlyCollection<IUser> Users => _users.Select(x => x.Value).ToArray();
        
        private ConcurrentDictionary<ulong, IChannel> _channels;
        private ConcurrentDictionary<ulong, IUser> _users;
        private int cacheLimit;
        
        public IReadOnlyCollection<T> GetChannels<T>() where T : IChannel
            => Channels.Where(x => x is T).Select(x => (T)x).ToArray();
        public IReadOnlyCollection<T> GetUsers<T>() where T : IUser
            => Users.Where(x => x is T).Select(x => (T)x).ToArray();
        
        public void AddChannel(IChannel channel)
        {
            _channels.AddOrUpdate(channel.Id, channel, (id, c) => c);
        }

        public void AddUser(IUser user)
        {
            _users.AddOrUpdate(user.Id, user, (id, u) => u);
        }

        public IChannel GetChannel(ulong channelId)
        {
            if (_channels.TryGetValue(channelId, out IChannel channel))
                return channel;
            return null;
        }

        public IUser GetUser(ulong userId)
        {
            if (_users.TryGetValue(userId, out IUser user))
                return user;
            return null;
        }

        public IChannel RemoveChannel(ulong channelId)
        {
            if (_channels.TryRemove(channelId, out IChannel channel))
                return channel;
            return null;
        }

        public IUser RemoveUser(ulong userId)
        {
            if (_users.TryRemove(userId, out IUser user))
                return user;
            return null;
        }
    }
}
