using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestStreamSummary
    {
        [JsonProperty("channels")]
        public int Channels { get; private set; }
        [JsonProperty("viewers")]
        public int Viewers { get; private set; }
    }
}
