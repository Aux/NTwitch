using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IChannel
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        uint Id { get; }
        string Name { get; }
        string DisplayName { get; }
        string Status { get; }
        string Game { get; }
        string Url { get; }
        string LogoUrl { get; }
        string BannerUrl { get; }
        string VideoBannerUrl { get; }
        string Background { get; }
        string ProfileBannerUrl { get; }
        string ProfileBackground { get; }
        string BroadcasterLanguage { get; }
        string Language { get; }
        int Delay { get; }
        int Views { get; }
        int Followers { get; }
        bool IsMature { get; }
        bool IsPartner { get; }
        string[] Links { get; }

        Task<IUserSubscription> GetSubscriberAsync(string name);
        Task<IEnumerable<IUserSubscription>> GetSubscribersAsync(SortDirection sort = SortDirection.Descending, int limit = 10, int page = 1);
        Task<IUserFollow> GetFollowAsync(string name);
        Task<IEnumerable<IUserFollow>> GetFollowersAsync(SortDirection sort = SortDirection.Descending, int limit = 10, int page = 1);
        Task<IEnumerable<IVideo>> GetVideosAsync(bool broadcasts = false, bool hls = false, int limit = 10, int page = 1);
        Task<IEnumerable<IBadge>> GetBadgesAsync();
    }
}
