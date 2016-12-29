using System;

namespace NTwitch
{
    public interface ITeamSummary : IEntity
    {
        string Background { get; }
        string BannerUrl { get; }
        DateTime CreatedAt { get; }
        string DisplayName { get; }
        string Info { get; }
        string LogoUrl { get; }
        string Name { get; }
        DateTime UpdatedAt { get; }
    }
}
