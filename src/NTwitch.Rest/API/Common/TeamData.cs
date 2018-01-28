using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class TeamData
    {
        [JsonProperty("teams")]
        public IReadOnlyCollection<Team> Teams { get; set; }
    }
}
