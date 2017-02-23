using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestFollow : RestEntity<ulong>
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }

        internal RestFollow(BaseRestClient client) : base(client) { }
    }
}
