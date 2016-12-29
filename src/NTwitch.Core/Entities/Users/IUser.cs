using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IUser : IEntity
    {
        string Bio { get; }
        DateTime CreatedAt { get; }
        string DisplayName { get; }
        string LogoUrl { get; }
        string Name { get; }
        string Type { get; }
        DateTime UpdatedAt { get; }

        /// <summary> Returns a collection of follow objects for all of the channels this user follows. </summary>
        Task<IEnumerable<IChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null);
        /// <summary> Returns a follow object if the user is following the specified channel. </summary>
        Task<IChannelFollow> GetFollowAsync(ulong userid);
        /// <summary> Blocks the target user. </summary>
        /// <remarks> Required scope: user_blocks_read </remarks>
        Task<IBlockedUser> BlockAsync();
        /// <summary> Unblocks the target user. </summary>
        /// <remarks> Required scope: user_blocks_read </remarks>
        Task<IBlockedUser> UnblockAsync();
    }
}
