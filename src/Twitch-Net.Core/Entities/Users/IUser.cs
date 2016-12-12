using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public interface IUser
    {
        /// <summary> The date and time this user was created. </summary>
        DateTime CreatedAt { get; }
        /// <summary> The date and time this user was last updated. </summary>
        DateTime UpdatedAt { get; }
        /// <summary> The unique idenifier for this user. </summary>
        uint Id { get; }
        /// <summary> This user's name. </summary>
        string Name { get; }
        /// <summary> This user's display name. </summary>
        string DisplayName { get; }
        /// <summary> This user's profile biography. </summary>
        string Bio { get; }
        /// <summary> This user's logo url. </summary>
        string LogoUrl { get; }
        /// <summary> Links to relevant api endpoints for this user. </summary>
        string[] Links { get; }

        Task<IChannelFollow> GetFollowAsync(string channel);
        Task<IChannelFollow> GetFollowAsync(IChannel channel);
        Task<IEnumerable<IChannel>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, SortDirection direction = SortDirection.Ascending, int limit = 10, int page = 1);
    }
}
