using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelSubscription : RestSubscription
    {
        [JsonProperty("channel")]
        public RestChannel Channel { get; private set; }

        public RestChannelSubscription(TwitchRestClient client) : base(client) { }

        public static new RestChannelSubscription Create(TwitchRestClient client, string json)
        {
            var sub = new RestChannelSubscription(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, sub);
            return sub;
        }
    }
}
