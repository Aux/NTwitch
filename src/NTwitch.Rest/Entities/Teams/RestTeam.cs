using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestTeam : RestTeamSummary, ITeam
    {
        [JsonProperty("")]
        public IEnumerable<RestUser> Users { get; internal set; }

        internal RestTeam(TwitchRestClient client) : base(client) { }

        new internal static RestTeam Create(BaseTwitchClient client, string json)
        {
            var team = new RestTeam(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, team);
            return team;
        }

        IEnumerable<IUser> ITeam.Users
            => Users;
    }
}
