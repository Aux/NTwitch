using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestTeamSummary : IEntity, ITeamSummary
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public string Background { get; internal set; }
        [JsonProperty("")]
        public string BannerUrl { get; internal set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("")]
        public string DisplayName { get; internal set; }
        [JsonProperty("")]
        public string Info { get; internal set; }
        [JsonProperty("")]
        public string LogoUrl { get; internal set; }
        [JsonProperty("")]
        public string Name { get; internal set; }
        [JsonProperty("")]
        public DateTime UpdatedAt { get; internal set; }

        internal RestTeamSummary(TwitchRestClient client)
        {
            Client = client;
        }

        internal static RestTeamSummary Create(BaseTwitchClient client, string json)
        {
            var team = new RestTeamSummary(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, team);
            return team;
        }
        ITwitchClient IEntity.Client
            => Client;
    }
}
