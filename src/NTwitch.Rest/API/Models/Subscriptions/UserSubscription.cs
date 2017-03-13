using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class UserSubscription : Subscription
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
