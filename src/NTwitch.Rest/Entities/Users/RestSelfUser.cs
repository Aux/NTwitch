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

        internal static new RestSelfUser Create(BaseRestClient client, string json)
        {
            var user = new RestSelfUser(client);
            JsonConvert.PopulateObject(json, user);
            return user;
        }

        // Chat
        public Task<IEnumerable<RestEmoteSet>> GetEmotesAsync()
            => UserHelper.GetEmotesAsync(this, Client);

        // Videos
        public Task<IEnumerable<RestStream>> GetFollowedStreamsAsync(StreamType type = StreamType.All, PageOptions options = null)
            => UserHelper.GetFollowedStreamsAsync(this, Client, type, options);
        public Task<IEnumerable<RestVideo>> GetFollowedVideosAsync(BroadcastType type = BroadcastType.Highlight, PageOptions options = null)
            => UserHelper.GetFollowedVideoAsync(this, Client, type, options);

        // Users
        public Task<RestChannelFollow> FollowAsync(ulong channelId, bool notify = false)
            => UserHelper.FollowAsync(this, Client, channelId, notify);
        public Task<RestChannelFollow> UnfollowAsync(ulong channelId)
            => UserHelper.UnfollowAsync(this, Client, channelId);
        public Task<IEnumerable<RestBlockedUser>> GetBlockedUsersAsync()
            => UserHelper.GetBlockedUsersAsync(this, Client);
        public Task<bool> IsSubscribedAsync(ulong channelId)
            => UserHelper.IsSubscribedAsync(this, Client, channelId);
        public Task<IEnumerable<RestChannelSubscription>> GetSubscriptionsAsync()
            => UserHelper.GetSubscriptionsAsync(this, Client);

        // ISelfUser
        Task ISelfUser.FollowAsync(ulong channelId, bool notify)
            => FollowAsync(channelId, notify);
        Task ISelfUser.UnfollowAsync(ulong channelId)
            => UnfollowAsync(channelId);
        Task<bool> ISelfUser.IsSubscribedAsync(ulong channelId)
            => IsSubscribedAsync(channelId);
        Task ISelfUser.GetBlockedUsersAsync()
            => GetBlockedUsersAsync();
        Task ISelfUser.GetEmotesAsync()
            => GetEmotesAsync();
        Task ISelfUser.GetFollowedStreamsAsync()
            => GetFollowedStreamsAsync();
        Task ISelfUser.GetFollowedVideosAsync()
            => GetFollowedVideosAsync();
        Task ISelfUser.GetSubscriptionsAsync()
            => GetSubscriptionsAsync();
    }
}
