using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestSubscription : RestEntity<ulong>
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }

        internal RestSubscription(BaseRestClient client) : base(client) { }

        internal static RestSubscription Create(BaseRestClient client, string json)
        {
            var sub = new RestSubscription(client);
            JsonConvert.PopulateObject(json, sub);
            return sub;
        }
    }
}
