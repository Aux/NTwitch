using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal class ChannelHelper
    {
        public static Task<IEnumerable<RestPost>> GetPostsAsync(RestChannel channel, int comments, TwitchPageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestPost> GetPostAsync(RestChannel channel, ulong id, int comments)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestUserFollow>> GetFollowersAsync(RestChannel channel, SortDirection direction, TwitchPageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestTeam>> GetTeamsAsync(RestChannel channel)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestVideo>> GetVideosAsync(RestChannel channel, string language, SortMode sort, BroadcastType type, TwitchPageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestPost> DeletePostAsync(RestSelfChannel channel, ulong postid)
        {
            throw new NotImplementedException();
        }

        public static Task<RestSelfChannel> ModifyAsync(RestSelfChannel channel, Action<ModifyChannelParams> args)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestUser>> GetEditorsAsynC(RestSelfChannel channel)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestUserSubscription>> GetSubscribersAsync(RestSelfChannel channel, SortDirection direction, TwitchPageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestUserSubscription> GetSubscriberAsync(RestSelfChannel channel, ulong userid)
        {
            throw new NotImplementedException();
        }

        public static Task<Task> StartCommercialAsync(RestSelfChannel channel, int duration)
        {
            throw new NotImplementedException();
        }

        public static Task<RestPost> CreatePostAsync(RestSelfChannel channel, Action<CreatePostParams> args)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestBadge>> GetBadgesAsync(RestChannel channel)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestEmoteSet>> GetEmoteSetAsync(RestChannel channel)
        {
            throw new NotImplementedException();
        }

        public static Task<RestEmoteSet> GetEmoteSetAsync(RestChannel channel, ulong setid)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelFollow> FollowAsync(RestChannel channel, bool notify)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelFollow> UnfollowAsync(RestChannel channel)
        {
            throw new NotImplementedException();
        }
    }
}
