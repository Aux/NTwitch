using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public interface IRestSimpleChannel : ISimpleChannel
    {
        // Channel
        Task ModifyAsync(Action<ModifyChannelParams> changes, RequestOptions options = null);

        // Chat
        Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(RequestOptions options = null);
        Task<RestChatBadges> GetChatBadgesAsync(RequestOptions options = null);

        // Streams
        Task<RestStream> GetStreamAsync(StreamType type = StreamType.Live, RequestOptions options = null);

        // Teams
        Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(RequestOptions options = null);
        
        // Users
        Task<RestUser> GetUserAsync(RequestOptions options = null);
        Task<RestSelfUser> GetSelfUserAsync(RequestOptions options = null);

        // Followers
        Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(bool ascending = false, PageOptions paging = null, RequestOptions options = null);
        Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(RequestOptions options = null);

        // Subscribers
        Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(bool ascending = false, PageOptions paging = null, RequestOptions options = null);
        Task<RestUserSubscription> GetSubscriberAsync(ulong userId, RequestOptions options = null);

        // Videos
        Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(PageOptions paging = null, RequestOptions options = null);
        Task<IReadOnlyCollection<RestClip>> GetClipsAsync(bool istrending = false, PageOptions paging = null, RequestOptions options = null);
    }
}
