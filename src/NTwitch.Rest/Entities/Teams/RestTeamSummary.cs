using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestTeamSummary : RestEntity
    {
        [JsonProperty("")]
        public string Background { get; private set; }
        [JsonProperty("")]
        public string BannerUrl { get; private set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; private set; }
        [JsonProperty("")]
        public string DisplayName { get; private set; }
        [JsonProperty("")]
        public string Info { get; private set; }
        [JsonProperty("")]
        public string LogoUrl { get; private set; }
        [JsonProperty("")]
        public string Name { get; private set; }
        [JsonProperty("")]
        public DateTime UpdatedAt { get; private set; }

        public RestTeamSummary(BaseRestClient client) : base(client) { }

        public static RestTeamSummary Create(BaseRestClient client, string json)
        {
            var team = new RestTeamSummary(client);
            JsonConvert.PopulateObject(json, team);
            return team;
        }
    }
}
