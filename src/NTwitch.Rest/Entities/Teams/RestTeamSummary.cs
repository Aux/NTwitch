using System;
using System.Runtime.CompilerServices;


namespace NTwitch.Rest
{
    public class RestTeamSummary : IEntity, ITeamSummary
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public string Background { get; internal set; }
        public string BannerUrl { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public string DisplayName { get; internal set; }
        public string Info { get; internal set; }
        public string LogoUrl { get; internal set; }
        public string Name { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }

        internal RestTeamSummary(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
