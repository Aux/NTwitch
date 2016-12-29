using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISelfUser : IEntity, IUser
    {
        string Email { get; }
        bool IsVerified { get; }
        TwitchNotifications Notifications { get; }
        bool IsPartnered { get; }
        bool IsTwitterConnected { get; }

        /// <summary> Gets a list of the emojis and emoticons that the user can use in chat. </summary>
        /// <remarks> Required scope: user_subscriptions </remarks>
        Task<IEnumerable<IEmoteSet>> GetEmotesAsync();
        /// <summary> Returns a subscription object if the user is subscribed to the specified channel. </summary>
        /// <remarks> Required scope: user_subscriptions </remarks>
        Task<IChannelSubscription> GetSubscriptionAsync(ulong channelid);
        /// <summary> Adds a specified user to the followers of a specified channel. </summary>
        /// <remarks> Required scope: user_follows_edit </remarks>
        Task<IChannelFollow> FollowAsync(ulong channelid, bool notify = false);
        /// <summary> Deletes a specified user from the followers of a specified channel. </summary>
        /// <remarks> Required scope: user_follows_edit </remarks>
        Task<IChannelFollow> UnfollowAsync(ulong channelid);
        /// <summary> Gets a user’s block list. List sorted by recency, newest first. </summary>
        /// <remarks> Required scope: user_blocks_read </remarks>
        Task<IEnumerable<IBlock>> GetBlockedUsersAsync();
        /// <summary> Gets the list of online streams a user follows based on the OAuth token provided. </summary>
        /// <remarks> Required scope: user_read </remarks>
        Task<IEnumerable<IStream>> GetFollowedStreamsAsync(StreamType type = StreamType.All, TwitchPageOptions options = null);
        /// <summary> Gets the videos from channels the user is following based on the OAuth token provided. </summary>
        /// <remarks> Required scope: user_read </remarks>
        Task<IEnumerable<IVideo>> GetFollowedVideosAsync(BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null);
    }
}
