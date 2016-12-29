using System;

namespace NTwitch.Rest
{
    public class RestTeamSummary : RestEntity, ITeamSummary
    {
        public string Background { get; }
        public string BannerUrl { get; }
        public DateTime CreatedAt { get; }
        public string DisplayName { get; }
        public string Info { get; }
        public string LogoUrl { get; }
        public string Name { get; }
        public DateTime UpdatedAt { get; }

        public RestTeamSummary(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
