using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelSubscription : RestSubscription
    {
        [JsonProperty("channel")]
        public RestChannel Channel { get; private set; }

        public RestChannelSubscription(BaseRestClient client) : base(client) { }

        public static new RestChannelSubscription Create(BaseRestClient client, string json)
        {
            var sub = new RestChannelSubscription(client);
            JsonConvert.PopulateObject(json, sub);
            return sub;
        }
    }
}
