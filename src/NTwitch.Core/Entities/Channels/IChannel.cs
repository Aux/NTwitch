using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IChannel : IEntity
    {
        string BroadcasterLanguage { get; }
        DateTime CreatedAt { get; }
        string DisplayName { get; }
        int FollowerCount { get; }
        string Game { get; }
        string Language { get; }
        string LogoUrl { get; }
        bool IsMature { get; }
        string Name { get; }
        bool IsPartner { get; }
        string ProfileBannerUrl { get; }
        string ProfileBackgroundColor { get; }
        string Status { get; }
        DateTime UpdatedAt { get; }
        string Url { get; }
        string VideoBannerUrl { get; }
        int ViewCount { get; }

        /// <summary> Gets a list of users who follow a specified channel, sorted by the date when they started following the channel. </summary>
        Task GetFollowersAsync(bool descending = true, TwitchPageOptions page = null);
        /// <summary> Gets a list of teams to which a specified channel belongs. </summary>
        Task GetTeamsAsync();
        /// <summary>  </summary>
        Task GetVideosAsync();
        /// <summary> Gets a list of badges that can be used in chat. </summary>
        Task<IEnumerable<IBadge>> GetBadgesAsync();
        /// <summary> Gets all chat emoticons (including their images). </summary>
        Task<IEnumerable<IEmote>> GetEmotesAsync();
        /// <summary> Gets all chat emoticons (not including their images). </summary>
        Task GetEmoteSetAsync(ulong setid);
        /// <summary>  </summary>
        Task<IStream> GetStreamAsync();
    }
}
