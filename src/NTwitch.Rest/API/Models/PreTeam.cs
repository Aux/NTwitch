using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class PreTeam
    {
        [JsonProperty("teams")]
        public IEnumerable<Team> Teams { get; set; }
    }
}
