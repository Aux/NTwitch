using Newtonsoft.Json;

namespace NTwitch
{
    public class TwitchLinks
    {
        [JsonProperty("")]
        public string Channel { get; internal set; }
        [JsonProperty("")]
        public string Users { get; internal set; }
        [JsonProperty("")]
        public string User { get; internal set; }
        [JsonProperty("")]
        public string Channels { get; internal set; }
        [JsonProperty("")]
        public string Chat { get; internal set; }
        [JsonProperty("")]
        public string Streams { get; internal set; }
        [JsonProperty("")]
        public string Ingests { get; internal set; }
        [JsonProperty("")]
        public string Teams { get; internal set; }
        [JsonProperty("")]
        public string Search { get; internal set; }
        [JsonProperty("self")]
        public string Self { get; internal set; }
    }
}
