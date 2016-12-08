using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
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

        Task GetVideosAsync();
        Task GetFollowsAsync();
    }
}
