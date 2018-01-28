using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class CommunityData
    {
        [JsonProperty("_cursor")]
        public string Cursor { get; set; }
        [JsonProperty("_total")]
        public int Total { get; set; }

        [JsonProperty("communities")]
        public Optional<IReadOnlyCollection<Community>> Communities { get; set; }
        [JsonProperty("moderators")]
        public Optional<IReadOnlyCollection<User>> Moderators { get; set; }
        [JsonProperty("timed_out_users")]
        public Optional<IReadOnlyCollection<User>> Timeouts { get; set; }
        [JsonProperty("banned_users")]
        public Optional<IReadOnlyCollection<User>> Bans { get; set; }
    }
}
