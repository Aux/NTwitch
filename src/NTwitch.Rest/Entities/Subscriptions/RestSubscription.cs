using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestSubscription : RestEntity<ulong>
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }

        internal RestSubscription(BaseRestClient client) : base(client) { }
    }
}
