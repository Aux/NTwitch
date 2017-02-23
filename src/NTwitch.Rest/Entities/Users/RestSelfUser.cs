using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestSelfUser : RestUser, ISelfUser
    {
        [JsonProperty("email")]
        public string Email { get; private set; }
        [JsonProperty("partnered")]
        public bool IsPartnered { get; private set; }
        [JsonProperty("twitter_connected")]
        public bool IsTwitterConnected { get; private set; }
        [JsonProperty("email_verified")]
        public bool IsVerified { get; private set; }
        [JsonProperty("notifications")]
        public TwitchNotifications Notifications { get; private set; }

        internal RestSelfUser(BaseRestClient client) : base(client) { }
        
        // Videos
        public Task<IEnumerable<RestStream>> GetFollowedStreamsAsync()
            => GetFollowedStreamsAsync(StreamType.All);
        public Task<IEnumerable<RestStream>> GetFollowedStreamsAsync(StreamType type = StreamType.All, PageOptions options = null)
            => UserHelper.GetFollowedStreamsAsync(this, Client, type, options);
        public Task<IEnumerable<RestVideo>> GetFollowedVideosAsync()
            => GetFollowedVideosAsync(BroadcastType.Highlight);
        public Task<IEnumerable<RestVideo>> GetFollowedVideosAsync(BroadcastType type = BroadcastType.Highlight, PageOptions options = null)
            => UserHelper.GetFollowedVideoAsync(this, Client, type, options);

        // Users
        public Task<RestBlockedUser> BlockAsync(uint userId)
            => UserHelper.BlockAsync(this, Client, userId);
        public Task UnblockAsync(uint userId)
            => UserHelper.BlockAsync(this, Client, userId);
        public Task<RestChannelFollow> FollowAsync(uint channelId)
            => FollowAsync(channelId, false);
        public Task<RestChannelFollow> FollowAsync(uint channelId, bool notify = false)
            => UserHelper.FollowAsync(this, Client, channelId, notify);
        public Task UnfollowAsync(uint channelId)
            => UserHelper.UnfollowAsync(this, Client, channelId);
        public Task<IEnumerable<RestBlockedUser>> GetBlockedUsersAsync()
            => GetBlockedUsersAsync(null);
        public Task<IEnumerable<RestBlockedUser>> GetBlockedUsersAsync(PageOptions options = null)
            => UserHelper.GetBlockedUsersAsync(this, Client, options);
        public Task<RestChannelSubscription> GetSubscriptionAsync(uint userId)
            => UserHelper.GetSubscriptionAsync(this, Client, userId);
        public Task<IEnumerable<RestChannelSubscription>> GetSubscriptionsAsync()
            => UserHelper.GetSubscriptionsAsync(this, Client);

        // ISelfUser
        Task ISelfUser.FollowAsync(uint channelId, bool notify)
            => FollowAsync(channelId, notify);
        Task ISelfUser.UnfollowAsync(uint channelId)
            => UnfollowAsync(channelId);
        Task ISelfUser.GetBlockedUsersAsync()
            => GetBlockedUsersAsync();
        Task ISelfUser.GetFollowedStreamsAsync()
            => GetFollowedStreamsAsync();
        Task ISelfUser.GetFollowedVideosAsync()
            => GetFollowedVideosAsync();
        Task ISelfUser.GetSubscriptionAsync(uint userId)
            => GetSubscriptionAsync(userId);
        Task ISelfUser.GetSubscriptionsAsync()
            => GetSubscriptionsAsync();
    }
}
