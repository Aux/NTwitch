using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestSelfUser : IEntity, ISelfUser
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public string Bio { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public string DisplayName { get; internal set; }
        public string Email { get; internal set; }
        public bool IsPartnered { get; internal set; }
        public bool IsTwitterConnected { get; internal set; }
        public bool IsVerified { get; internal set; }
        public string LogoUrl { get; internal set; }
        public string Name { get; internal set; }
        public TwitchNotifications Notifications { get; internal set; }
        public string Type { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }

        internal RestSelfUser(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

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
        ITwitchClient IEntity.Client
            => Client;
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
