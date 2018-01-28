using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class FollowData
    {
        [JsonProperty("_cursor")]
        public string Cursor { get; set; }
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("follows")]
        public IReadOnlyCollection<Follow> Follows { get; set; }
    }
}
