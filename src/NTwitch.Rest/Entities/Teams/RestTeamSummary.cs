using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestTeamSummary : RestEntity
    {
        [JsonProperty("background")]
        public string Background { get; private set; }
        [JsonProperty("banner")]
        public string BannerUrl { get; private set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; private set; }
        [JsonProperty("info")]
        public string Info { get; private set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("updated_at")]
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
