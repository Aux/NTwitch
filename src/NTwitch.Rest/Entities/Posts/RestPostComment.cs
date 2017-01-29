using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestPostComment
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        [JsonProperty("body")]
        public string Body { get; internal set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("emotes")]
        public IEnumerable<RestPostEmote> Emotes { get; internal set; }
        [JsonProperty("deleted")]
        public bool IsDeleted { get; internal set; }
        [JsonProperty("permissions")]
        public RestPostPermissions Permissions { get; internal set; }
        [JsonProperty("reactions")]
        public IEnumerable<RestPostReaction> Reactions { get; internal set; }
        [JsonProperty("user")]
        public RestUser User { get; internal set; }
    }
}
