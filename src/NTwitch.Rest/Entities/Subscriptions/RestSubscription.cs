using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestSubscription : SubscriptionBase
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }

        internal RestSubscription(TwitchRestClient client) : base(client) { }

        internal static RestSubscription Create(TwitchRestClient client, string json)
        {
            var sub = new RestSubscription(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, sub);
            return sub;
        }
    }
}
