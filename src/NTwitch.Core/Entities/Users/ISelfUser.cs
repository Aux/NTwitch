using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<IChannelSubscription> GetSubscriptionAsync(ulong id);
        /// <summary> Returns a collection of follow objects for all of the channels this user follows. </summary>
        Task<IEnumerable<IChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null);
        /// <summary> Returns a follow object if the user is following the specified channel. </summary>
        Task<IChannelFollow> GetFollowAsync(ulong id);
        /// <summary> Adds a specified user to the followers of a specified channel. </summary>
        /// <remarks> Required scope: user_follows_edit </remarks>
        Task<IChannelFollow> FollowAsync(ulong id, bool notify = false);
        /// <summary> Deletes a specified user from the followers of a specified channel. </summary>
        /// <remarks> Required scope: user_follows_edit </remarks>
        Task<IChannelFollow> UnfollowAsync(ulong id);
        /// <summary> Gets a user’s block list. List sorted by recency, newest first. </summary>
        /// <remarks> Required scope: user_blocks_read </remarks>
        Task<IEnumerable<IBlock>> GetBlockedUsersAsync();
        /// <summary> Blocks the target user. </summary>
        /// <remarks> Required scope: user_blocks_read </remarks>
        Task<IBlock> BlockAsync(ulong id);
        /// <summary> Unblocks the target user. </summary>
        /// <remarks> Required scope: user_blocks_read </remarks>
        Task<IBlock> UnblockAsync(ulong id);
    }
}
