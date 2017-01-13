using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestPostComment
    {
        [JsonProperty("")]
        public string Body { get; internal set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("")]
        public IEnumerable<RestPostEmote> Emotes { get; internal set; }
        [JsonProperty("")]
        public bool IsDeleted { get; internal set; }
        [JsonProperty("")]
        public RestPostPermissions Permissions { get; internal set; }
        [JsonProperty("")]
        public IEnumerable<RestPostReaction> Reactions { get; internal set; }
        [JsonProperty("")]
        public RestUser User { get; internal set; }
    }
}
