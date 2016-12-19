using Newtonsoft.Json;

namespace NTwitch
{
    public class TwitchLinks
    {
        public string Channel { get; }
        public string Users { get; }
        public string User { get; }
        public string Channels { get; }
        public string Chat { get; }
        public string Streams { get; }
        public string Ingests { get; }
        public string Teams { get; }
        public string Search { get; }
        [JsonProperty("self")]
        public string Self { get; }
    }
}
