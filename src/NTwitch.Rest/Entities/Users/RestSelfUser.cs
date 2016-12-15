using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public class RestSelfUser : ISelfUser
    {
        public TwitchRestClient Client { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public uint Id { get; }
        public string Name { get; }
        public string DisplayName { get; }
        public string Email { get; }
        public string Bio { get; }
        public string LogoUrl { get; }
        public string[] Links { get; }
        public bool IsPartnered { get; }
        public bool[] Notifications { get; }
        
        public Task<IEnumerable<Emoticon>> GetEmotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IStream>> GetStreamsAsync(StreamType type = StreamType.All, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task GetVideosAsync(BroadcastType type = BroadcastType.All, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task SetNotificationAsync(string channel, bool notify)
        {
            throw new NotImplementedException();
        }

        public Task SetNotificationAsync(IChannel channel, bool notify)
        {
            throw new NotImplementedException();
        }

        public Task AddFollow(string channel)
        {
            throw new NotImplementedException();
        }

        public Task AddFollow(IChannel channel)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFollow(string name)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFollow(IChannel name)
        {
            throw new NotImplementedException();
        }

        public Task<IChannelSubscription> GetSubscriptionAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IChannelFollow> GetFollowAsync(string channel)
        {
            throw new NotImplementedException();
        }

        public Task<IChannelFollow> GetFollowAsync(IChannel channel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IChannel>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, SortDirection direction = SortDirection.Ascending, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
            => DisplayName;
    }
}
