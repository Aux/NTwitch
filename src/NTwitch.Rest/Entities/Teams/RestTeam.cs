using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestTeam : RestTeamSummary
    {
        [JsonProperty("")]
        public IEnumerable<RestUser> Users { get; private set; }

        public RestTeam(TwitchRestClient client) : base(client) { }

        public static new RestTeam Create(BaseRestClient client, string json)
        {
            var team = new RestTeam(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, team);
            return team;
        }
    }
}
