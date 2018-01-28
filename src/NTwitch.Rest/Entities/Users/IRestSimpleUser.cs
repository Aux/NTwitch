using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public interface IRestSimpleUser : ISimpleUser
    {
        // Channels
        Task<RestChannel> GetChannelAsync(RequestOptions options = null);

        // Chat
        Task<IReadOnlyDictionary<string, IReadOnlyCollection<RestEmote>>> GetEmotesAsync(RequestOptions options = null);

        // Follows
        Task<IReadOnlyCollection<RestChannelFollow>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, bool ascending = false, PageOptions paging = null, RequestOptions options = null);
        Task<RestChannelFollow> GetFollowAsync(ulong channelId, RequestOptions options = null);

        // Streams
        Task<RestBroadcast> GetStreamAsync(StreamType type = StreamType.Live, RequestOptions options = null);
        Task<IReadOnlyCollection<RestBroadcast>> GetFollowedStreamsAsync(StreamType type = StreamType.Live, PageOptions paging = null, RequestOptions options = null);

        // Subscriptions
        Task<RestChannelSubscription> GetSubscriptionAsync(ulong channelId, RequestOptions options = null);

        // Users
        Task BlockAsync(RequestOptions options = null);
        Task UnblockAsync(RequestOptions options = null);
        Task<IReadOnlyCollection<RestBlockedUser>> GetBlocksAsync(PageOptions paging = null, RequestOptions options = null);

        // Videos
        Task<IReadOnlyCollection<RestClip>> GetFollowedClipsAsync(bool istrending = false, PageOptions paging = null, RequestOptions options = null);
    }
}
