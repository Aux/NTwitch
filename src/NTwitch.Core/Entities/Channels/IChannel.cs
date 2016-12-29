using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IChannel : IEntity, IChannelSummary
    {
        string BroadcasterLanguage { get; }
        DateTime CreatedAt { get; }
        int FollowerCount { get; }
        string Game { get; }
        string Language { get; }
        string LogoUrl { get; }
        bool IsMature { get; }
        bool IsPartner { get; }
        string ProfileBannerUrl { get; }
        string ProfileBackgroundColor { get; }
        string Status { get; }
        DateTime UpdatedAt { get; }
        string Url { get; }
        string VideoBannerUrl { get; }
        int ViewCount { get; }
        
        /// <summary> Gets posts from the channel feed. </summary>
        Task<IEnumerable<IPost>> GetPostsAsync(int comments = 5, TwitchPageOptions options = null);
        /// <summary> Gets a specified post from the channel feed. </summary> 
        Task<IPost> GetPostAsync(ulong id, int comments = 5);
        /// <summary> Gets a list of users who follow a specified channel, sorted by the date when they started following the channel. </summary> 
        Task<IEnumerable<IUserFollow>> GetFollowersAsync(SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null);
        /// <summary> Gets a list of teams to which a specified channel belongs. </summary> 
        Task<IEnumerable<ITeam>> GetTeamsAsync();
        /// <summary>  </summary> 
        Task<IEnumerable<IVideo>> GetVideosAsync();
    }
}
