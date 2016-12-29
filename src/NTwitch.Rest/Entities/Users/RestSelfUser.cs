using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestSelfUser : RestEntity, ISelfUser
    {
        public string Bio { get; }
        public DateTime CreatedAt { get; }
        public string DisplayName { get; }
        public string Email { get; }
        public bool IsPartnered { get; }
        public bool IsTwitterConnected { get; }
        public bool IsVerified { get; }
        public string LogoUrl { get; }
        public string Name { get; }
        public TwitchNotifications Notifications { get; }
        public string Type { get; }
        public DateTime UpdatedAt { get; }
        
        public RestSelfUser(TwitchRestClient client, ulong id) : base(client, id) { }

        public Task<RestBlockedUser> BlockAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestChannelFollow> FollowAsync(ulong channelid, bool notify = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestBlockedUser>> GetBlockedUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestEmoteSet>> GetEmotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestChannelFollow> GetFollowAsync(ulong userid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestStream>> GetFollowedStreamsAsync(StreamType type = StreamType.All, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestVideo>> GetFollowedVideosAsync(BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<RestChannelSubscription> GetSubscriptionAsync(ulong channelid)
        {
            throw new NotImplementedException();
        }

        public Task<RestBlockedUser> UnblockAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestChannelFollow> UnfollowAsync(ulong channelid)
        {
            throw new NotImplementedException();
        }

        //ISelfUser

        async Task<IEnumerable<IEmoteSet>> ISelfUser.GetEmotesAsync()
            => await GetEmotesAsync();
        async Task<IChannelSubscription> ISelfUser.GetSubscriptionAsync(ulong channelid)
            => await GetSubscriptionAsync(channelid);
        async Task<IChannelFollow> ISelfUser.FollowAsync(ulong channelid, bool notify)
            => await FollowAsync(channelid, notify);
        async Task<IChannelFollow> ISelfUser.UnfollowAsync(ulong channelid)
            => await UnfollowAsync(channelid);
        async Task<IEnumerable<IBlockedUser>> ISelfUser.GetBlockedUsersAsync()
            => await GetBlockedUsersAsync();
        async Task<IEnumerable<IStream>> ISelfUser.GetFollowedStreamsAsync(StreamType type, TwitchPageOptions options)
            => await GetFollowedStreamsAsync(type, options);
        async Task<IEnumerable<IVideo>> ISelfUser.GetFollowedVideosAsync(BroadcastType type, TwitchPageOptions options)
            => await GetFollowedVideosAsync(type, options);
        async Task<IEnumerable<IChannelFollow>> IUser.GetFollowsAsync(SortMode mode, SortDirection direction, TwitchPageOptions options)
            => await GetFollowsAsync(mode, direction, options);
        async Task<IChannelFollow> IUser.GetFollowAsync(ulong userid)
            => await GetFollowAsync(userid);
        async Task<IBlockedUser> IUser.BlockAsync()
            => await BlockAsync();
        async Task<IBlockedUser> IUser.UnblockAsync()
            => await UnblockAsync();
    }
}
