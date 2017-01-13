using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ChannelHelper
    {
        public static Task<IEnumerable<RestPost>> GetPostsAsync(ChannelBase channel, int comments, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestClip> GetClipAsync(ChannelBase channel, string id)
        {
            throw new NotImplementedException();
        }

        public static Task<RestClip> GetTopClipsAsync(ChannelBase channel, string game, VideoPeriod period, bool istrending)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelFollow> UnfollowAsync(ChannelBase channel)
        {
            throw new NotImplementedException();
        }

        public static Task<RestPost> CreatePostAsync(RestSelfChannel channel, Action<CreatePostParams> args)
        {
            throw new NotImplementedException();
        }

        public static Task<RestPost> DeletePostAsync(RestSelfChannel channel, ulong postid)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestUser>> GetEditorsAsynC(RestSelfChannel channel)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestUserSubscription>> GetSubscribersAsync(RestSelfChannel channel, SortDirection direction, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestUserSubscription> GetSubscriberAsync(RestSelfChannel channel, ulong userid)
        {
            throw new NotImplementedException();
        }

        public static Task StartCommercialAsync(RestSelfChannel restSelfChannel, int duration)
        {
            throw new NotImplementedException();
        }

        public static Task<RestSelfChannel> ResetStreamKeyAsync(RestSelfChannel channel)
        {
            throw new NotImplementedException();
        }

        public static Task<RestSelfChannel> ModifyAsync(RestSelfChannel channel, Action<ModifyChannelParams> args)
        {
            throw new NotImplementedException();
        }

        public static Task<RestPost> GetPostAsync(ChannelBase channel, ulong id, int comments)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannelFollow> FollowAsync(ChannelBase channel, bool notify)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestUserFollow>> GetFollowersAsync(ChannelBase channel, SortDirection direction, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestEmote>> GetEmotesAsync(ChannelBase channel)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestTeam>> GetTeamsAsync(ChannelBase channel)
        {
            throw new NotImplementedException();
        }

        public static Task<RestEmoteSet> GetEmoteSetAsync(ChannelBase channel, ulong setid)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestVideo>> GetVideosAsync(ChannelBase channel, string language, SortMode sort, BroadcastType type, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestEmoteSet>> GetEmoteSetAsync(ChannelBase channel)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestBadges>> GetBadgesAsync(ChannelBase channel)
        {
            throw new NotImplementedException();
        }
    }
}
