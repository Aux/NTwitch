using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IUser : IEntity
    {
        string Bio { get; set; }
        DateTime CreatedAt { get; set; }
        string DisplayName { get; set; }
        string LogoUrl { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        DateTime UpdatedAt { get; set; }

        /// <summary> Returns a collection of follow objects for all of the channels this user follows. </summary>
        Task<IEnumerable<IChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null);
        /// <summary> Returns a follow object if the user is following the specified channel. </summary>
        Task<IChannelFollow> GetFollowAsync(ulong id);
    }
}
