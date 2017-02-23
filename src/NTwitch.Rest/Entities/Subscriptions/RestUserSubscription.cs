using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestUserSubscription : RestSubscription
    {
        [JsonProperty("user")]
        public RestUser User { get; private set; }

        public RestUserSubscription(BaseRestClient client) : base(client) { }
    }
}
