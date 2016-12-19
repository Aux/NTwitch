using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestGame : IGame
    {
        [JsonProperty("_id")]
        public uint Id { get; internal set; }
        [JsonProperty("giantbomb_id")]
        public uint GiantBombId { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("box")]
        public TwitchImage BoxArt { get; internal set; }
        [JsonProperty("logo")]
        public TwitchImage Logo { get; internal set; }
    }
}
