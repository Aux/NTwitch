using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class SelfUserBase : UserBase
    {
        internal SelfUserBase(BaseRestClient client) : base(client) { }
        
        public Task<RestChannelFollow> FollowAsync(ulong channelid, bool notify = false)
            => UserHelper.FollowAsync(this, channelid, notify);

        public Task<IEnumerable<RestBlockedUser>> GetBlockedUsersAsync()
            => UserHelper.GetBlockedUsersAsync(this);

        public Task<IEnumerable<RestEmoteSet>> GetEmotesAsync()
            => UserHelper.GetEmotesAsync(this);
        
        public Task<IEnumerable<RestStream>> GetFollowedStreamsAsync(StreamType type = StreamType.All, PageOptions options = null)
            => UserHelper.GetFollowedStreamsAsync(this, type, options);

        public Task<IEnumerable<RestVideo>> GetFollowedVideosAsync(BroadcastType type = BroadcastType.Highlight, PageOptions options = null)
            => UserHelper.GetFollowedVideoAsync(this, type, options);
        
        public Task<RestChannelSubscription> GetSubscriptionAsync(ulong channelid)
            => UserHelper.GetSubscriptionAsync(this, channelid);
        
        public Task<RestChannelFollow> UnfollowAsync(ulong channelid)
            => UserHelper.UnfollowAsync(this, channelid);
    }
}
