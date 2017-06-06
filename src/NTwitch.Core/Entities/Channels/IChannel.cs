using System;

namespace NTwitch
{
    public interface IChannel : IEntity<ulong>, IUpdateable
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        string Status { get; }
        string BroadcasterLanguage { get; }
        string Game { get; }
        string Language { get; }
        string LogoUrl { get; }
        string VideoBannerUrl { get; }
        string ProfileBannerUrl { get; }
        string ProfileBannerBackgroundColor { get; }
        string Url { get; }
        bool IsMature { get; }
        bool IsPartner { get; }
        uint Views { get; }
        uint Followers { get; }
    }
}
