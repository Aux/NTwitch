using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IUser : IEntity
    {
        /// <summary> The display name of this user. </summary>
        string DisplayName { get; }

        /// <summary> Block this user. </summary>
        Task<IBlockedUser> BlockAsync();
        /// <summary> Unblock this user. </summary>
        Task UnblockAsync();
        /// <summary> Check if this user is following the specified channel. </summary>
        Task<IChannelFollow> GetFollowAsync(uint channelId);
        /// <summary> Get the channels this user is following. </summary>
        Task<IEnumerable<IChannelFollow>> GetFollowsAsync();
    }
}
