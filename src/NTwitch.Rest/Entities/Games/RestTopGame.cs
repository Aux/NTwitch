using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestTopGame
    {
        [JsonProperty("game")]
        public RestGame Game { get; internal set; }
        [JsonProperty("channels")]
        public int Channels { get; internal set; }
        [JsonProperty("viewers")]
        public int Viewers { get; internal set; }
    }
}
