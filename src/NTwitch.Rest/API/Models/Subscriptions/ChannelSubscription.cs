using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    public class ChannelSubscription : Subscription
    {
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
