using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NTwitch.Pubsub
{
    public class PubsubCache : IPubsubCache
    {
        public IReadOnlyCollection<PubsubChannel> Channels => _channels.Select(x => x.Value).ToList();
        public IReadOnlyCollection<PubsubUser> Users => _users.Select(x => x.Value).ToList();

        private ConcurrentDictionary<ulong, PubsubChannel> _channels;
        private ConcurrentDictionary<ulong, PubsubUser> _users;

        public PubsubCache()
        {
            _channels = new ConcurrentDictionary<ulong, PubsubChannel>();
            _users = new ConcurrentDictionary<ulong, PubsubUser>();
        }

        public PubsubChannel GetChannel(ulong channelId)
        {
            if (_channels.TryGetValue(channelId, out PubsubChannel channel))
                return channel;
            return null;
        }

        public PubsubUser GetUser(ulong userId)
        {
            if (_users.TryGetValue(userId, out PubsubUser user))
                return user;
            return null;
        }

        public void AddChannel(PubsubChannel channel)
        {
            _channels.AddOrUpdate(channel.Id, channel, (id, c) => c);
        }

        public void AddUser(PubsubUser user)
        {
            _users.AddOrUpdate(user.Id, user, (id, u) => u);
        }
        
        public PubsubChannel RemoveChannel(ulong channelId)
        {
            if (_channels.TryRemove(channelId, out PubsubChannel channel))
                return channel;
            return null;
        }

        public PubsubUser RemoveUser(ulong userId)
        {
            if (_users.TryRemove(userId, out PubsubUser user))
                return user;
            return null;
        }

        #region ICache
        IReadOnlyCollection<PubsubChannel> IPubsubCache.Channels
            => throw new NotImplementedException();
        IReadOnlyCollection<PubsubUser> IPubsubCache.Users
            => throw new NotImplementedException();
        IReadOnlyCollection<IChannel> ICache.Channels
            => throw new NotImplementedException();
        IReadOnlyCollection<IUser> ICache.Users
            => throw new NotImplementedException();

        PubsubChannel IPubsubCache.GetChannel(ulong channelId)
            => throw new NotImplementedException();
        PubsubUser IPubsubCache.GetUser(ulong userId)
            => throw new NotImplementedException();
        void IPubsubCache.AddChannel(PubsubChannel channel)
            => throw new NotImplementedException();
        void IPubsubCache.AddUser(PubsubUser user)
            => throw new NotImplementedException();
        PubsubChannel IPubsubCache.RemoveChannel(ulong channelId)
            => throw new NotImplementedException();
        PubsubUser IPubsubCache.RemoveUser(ulong userId)
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
