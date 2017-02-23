using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestTeam : RestTeamSummary
    {
        [JsonProperty("users")]
        public IEnumerable<RestUser> Users { get; private set; }

        public RestTeam(BaseRestClient client) : base(client) { }
    }
}
