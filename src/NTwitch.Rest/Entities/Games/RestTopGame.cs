using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestTopGame : RestEntity<ulong>
    {
        [JsonProperty("channels")]
        public int ChannelTotal { get; internal set; }
        [JsonProperty("game")]
        public RestGame Game { get; internal set; }
        [JsonProperty("viewers")]
        public int ViewerTotal { get; internal set; }

        public RestTopGame(BaseRestClient client) : base(client) { }
    }
}
