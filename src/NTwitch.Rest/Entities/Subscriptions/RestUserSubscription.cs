using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestUserSubscription : RestSubscription
    {
        [JsonProperty("user")]
        public RestUser User { get; private set; }

        public RestUserSubscription(TwitchRestClient client) : base(client) { }

        public static new RestUserSubscription Create(TwitchRestClient client, string json)
        {
            var sub = new RestUserSubscription(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, sub);
            return sub;
        }
    }
}
