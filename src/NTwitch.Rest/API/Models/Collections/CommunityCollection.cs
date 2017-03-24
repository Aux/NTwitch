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
        [JsonProperty("moderators")]
        public IEnumerable<User> Moderators { get; set; }
        [JsonProperty("timed_out_users")]
        public IEnumerable<User> Timeouts { get; set; }
        [JsonProperty("banned_users")]
        public IEnumerable<User> Bans { get; set; }
    }
}
