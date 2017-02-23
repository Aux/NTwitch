using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelSubscription : RestSubscription
    {
        [JsonProperty("channel")]
        public RestChannel Channel { get; private set; }

        public RestChannelSubscription(BaseRestClient client) : base(client) { }
    }
}
