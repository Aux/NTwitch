using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestUserFollow : RestFollow
    {
        [JsonProperty("user")]
        public RestUser User { get; private set; }

        public RestUserFollow(BaseRestClient client) : base(client) { }
    }
}
