using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestUserFollow : RestFollow
    {
        [JsonProperty("user")]
        public RestUser User { get; private set; }

        public RestUserFollow(TwitchRestClient client) : base(client) { }

        public static new RestUserFollow Create(TwitchRestClient client, string json)
        {
            var follow = new RestUserFollow(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, follow);
            return follow;
        }
    }
}
