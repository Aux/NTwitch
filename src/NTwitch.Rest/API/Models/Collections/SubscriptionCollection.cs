using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class SubscriptionCollection
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("subscriptions")]
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}
