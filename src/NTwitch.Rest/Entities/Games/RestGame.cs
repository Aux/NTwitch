using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestGame : RestEntity<ulong>
    {
        [JsonProperty("box")]
        public TwitchImage Box { get; private set; }
        [JsonProperty("giantbomb_id")]
        public uint GiantbombId { get; private set; }
        [JsonProperty("logo")]
        public TwitchImage Logo { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("popularity")]
        public int Popularity { get; private set; }

        public RestGame(BaseRestClient client) : base(client) { }
    }
}
