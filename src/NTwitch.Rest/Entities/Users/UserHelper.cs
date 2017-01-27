using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class UserHelper
    {
        public static Task<IEnumerable<RestBlockedUser>> GetBlockedUsersAsync(ISelfUser user, BaseRestClient client)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestEmoteSet>> GetEmotesAsync(ISelfUser user, BaseRestClient client)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelFollow> FollowAsync(ISelfUser user, BaseRestClient client, ulong channelid, bool notify)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestStream>> GetFollowedStreamsAsync(ISelfUser user, BaseRestClient client, StreamType type, PageOptions options)
        {
            throw new NotImplementedException();
        }

        internal static Task<RestBlockedUser> BlockAsync(RestUserSummary restUserSummary, BaseRestClient client)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> IsFollowingAsync(RestUserSummary restUserSummary, BaseRestClient client, ulong channelId)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelSubscription> GetSubscriptionAsync(ISelfUser user, BaseRestClient client, ulong channelid)
        {
            throw new NotImplementedException();
        }

        internal static Task<RestBlockedUser> UnblockAsync(RestUserSummary restUserSummary, BaseRestClient client)
        {
            throw new NotImplementedException();
        }

        internal static Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(RestUserSummary restUserSummary, BaseRestClient client, SortMode mode, SortDirection direction, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelFollow> UnfollowAsync(ISelfUser user, BaseRestClient client, ulong channelid)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestVideo>> GetFollowedVideoAsync(ISelfUser user, BaseRestClient client, BroadcastType type, PageOptions options)
        {
            throw new NotImplementedException();
        }

        internal static Task<bool> IsSubscribedAsync(ISelfUser user, BaseRestClient client, ulong channelId)
        {
            throw new NotImplementedException();
        }

        internal static Task<IEnumerable<RestChannelSubscription>> GetSubscriptionsAsync(ISelfUser user, BaseRestClient client)
        {
            throw new NotImplementedException();
        }
    }
}
