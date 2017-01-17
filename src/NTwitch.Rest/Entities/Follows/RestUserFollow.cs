using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestUserFollow : RestFollow
    {
        [JsonProperty("user")]
        public RestUser User { get; private set; }

        public RestUserFollow(BaseRestClient client) : base(client) { }

        public static new RestUserFollow Create(BaseRestClient client, string json)
        {
            var follow = new RestUserFollow(client);
            JsonConvert.PopulateObject(json, follow);
            return follow;
        }
    }
}
