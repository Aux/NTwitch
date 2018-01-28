using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class SubscriptionData
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("subscriptions")]
        public IReadOnlyCollection<Subscription> Subscriptions { get; set; }
    }
}
