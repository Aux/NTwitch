using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IUser : IEntity<ulong>
    {
        string DisplayName { get; }

        //// Communities
        //Task<IUserCommunity> GetUserCommunityAsync(string communityId, bool isName = false);

        //// Channels
        //Task<IChannel> GetChannelAsync();

        //// Chat
        //Task<IReadOnlyDictionary<string, IEnumerable<IEmote>>> GetEmotesAsync();

        //// Streams
        //Task<IStream> GetStreamAsync(StreamType type = StreamType.Live);
        //Task<IReadOnlyCollection<IStream>> GetFollowedStreamAsync(StreamType type = StreamType.Live);

        //// Clips
        //Task<IReadOnlyCollection<IClip>> GetClipsAsynC(bool trending = false);

        //// Subscriptions
        //Task<IReadOnlyCollection<IChannelSubscription>> GetSubscriptionsAsync();
        //Task<IChannelSubscription> GetSubscriptionAsync(ulong channelId);

        //// Follows
        //Task<IReadOnlyCollection<IChannelFollow>> GetFollowsAsync(SortMode sortMode = SortMode.CreatedAt, bool ascending = false);
        //Task<IChannelFollow> GetFollowAsync(ulong channelId);
        //Task<IReadOnlyCollection<IUserFollow>> GetFollowersAsync();
        //Task<IUserFollow> GetFollowerAsync(ulong userId);

        //// Blocks
        //Task<IReadOnlyCollection<IBlockedUser>> GetBlocksAsync();
        //Task BlockAsync();
        //Task UnblockAsync();

        //// VHS
        //Task<string> CreateHeartbeatAsync();
        //Task<string> GetHeartbeatAsync();
        //Task DeleteHeartbeatAsync();


    }
}
