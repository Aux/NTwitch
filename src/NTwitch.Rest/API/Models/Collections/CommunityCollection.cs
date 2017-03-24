using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class CommunityCollection
    {
        [JsonProperty("_cursor")]
        public string Cursor { get; set; }
        [JsonProperty("_total")]
        public int Total { get; set; }
        [JsonProperty("communities")]
        public IEnumerable<Community> Communities { get; set; }
    }
}
