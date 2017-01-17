using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class UserHelper
    {
        public static Task<IEnumerable<RestBlockedUser>> GetBlockedUsersAsync(SelfUserBase selfUserBase)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestEmoteSet>> GetEmotesAsync(SelfUserBase selfUserBase)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelFollow> FollowAsync(SelfUserBase selfUserBase, ulong channelid, bool notify)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestStream>> GetFollowedStreamsAsync(SelfUserBase selfUserBase, StreamType type, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelSubscription> GetSubscriptionAsync(SelfUserBase selfUserBase, ulong channelid)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelFollow> UnfollowAsync(SelfUserBase selfUserBase, ulong channelid)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestVideo>> GetFollowedVideoAsync(SelfUserBase selfUserBase, BroadcastType type, PageOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
