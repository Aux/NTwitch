using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestUserSubscription : RestSubscription
    {
        [JsonProperty("user")]
        public RestUser User { get; private set; }

        public RestUserSubscription(BaseRestClient client) : base(client) { }

        public static new RestUserSubscription Create(BaseRestClient client, string json)
        {
            var sub = new RestUserSubscription(client);
            JsonConvert.PopulateObject(json, sub);
            return sub;
        }
    }
}
