using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelFollow : RestFollow
    {
        [JsonProperty("channel")]
        public RestChannel Channel { get; private set; }

        public RestChannelFollow(BaseRestClient client) : base(client) { }
    }
}
