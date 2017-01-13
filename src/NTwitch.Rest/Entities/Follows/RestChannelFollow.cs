using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelFollow : RestFollow
    {
        [JsonProperty("channel")]
        public RestChannel Channel { get; private set; }

        public RestChannelFollow(BaseRestClient client) : base(client) { }

        public static new RestChannelFollow Create(TwitchRestClient client, string json)
        {
            var follow = new RestChannelFollow(client);
            JsonConvert.PopulateObject(json, follow);
            return follow;
        }
    }
}
