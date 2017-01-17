using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestPost : PostBase
    {
        [JsonProperty("")]
        public string Body { get; internal set; }
        [JsonProperty("")]
        public IEnumerable<RestPostComment> Comments { get; internal set; }
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

        internal RestPost(BaseRestClient client) : base(client) { }

        internal static RestPost Create(BaseRestClient client, string json)
        {
            var post = new RestPost(client);
            JsonConvert.PopulateObject(json, post);
            return post;
        }
    }
}
