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

        /// <summary>  </summary>
        /// <summary> Gets posts from the channel feed. </summary>
        Task<IEnumerable<IPost>> GetPostsAsync(int comments = 5, TwitchPageOptions options = null);
        /// <summary> Gets a specified post from the channel feed. </summary> 
        Task<IPost> GetPostAsync(ulong id, int comments = 5);
        /// <summary> Gets a list of users who follow a specified channel, sorted by the date when they started following the channel. </summary> 
        Task<IEnumerable<IUserFollow>> GetFollowersAsync(SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null);
        /// <summary> Gets a list of teams to which a specified channel belongs. </summary> 
        Task<IEnumerable<ITeam>> GetTeamsAsync();
        /// <summary> Gets a list of videos from a specified channel. </summary> 
        Task<IEnumerable<IVideo>> GetVideosAsync(string language = null, SortMode sort = SortMode.CreatedAt, BroadcastType type = BroadcastType.Highlight, TwitchPageOptions options = null);
        /// <summary> Gets a list of badges that can be used in chat for a specified channel. </summary> 
        Task<IEnumerable<IBadge>> GetBadgesAsync();
        /// <summary> Gets all chat emoticon sets (not including their images). </summary>
        Task<IEnumerable<IEmoteSet>> GetEmoteSetsAsync();
        /// <summary> Gets all chat emoticons (not including their images) in a specified set. </summary>
        Task<IEmoteSet> GetEmoteSetAsync(ulong setid);
        /// <summary> Gets all chat emoticons (including their images). </summary>
        Task<IEnumerable<IEmote>> GetEmotesAsync();
        /// <summary> Gets stream information (the stream object) for a specified channel. </summary>
        Task<IStream> GetStreamAsync();
        /// <summary> Adds a specified user to the followers of a specified channel. </summary>
        /// <remarks> Required scope: user_follows_edit </remarks>
        Task<IChannelFollow> FollowAsync(bool notify = false);
        /// <summary> Deletes a specified user from the followers of a specified channel. </summary>
        /// <remarks> Required scope: user_follows_edit </remarks>
        Task<IChannelFollow> UnfollowAsync();
    }
}
