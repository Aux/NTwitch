using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestIngest
    {
        [JsonProperty("")]
        public double Availability { get; private set; }
        [JsonProperty("")]
        public bool IsDefault { get; private set; }
        [JsonProperty("")]
        public string Name { get; private set; }
        [JsonProperty("")]
        public string UrlTemplate { get; private set; }
    }
}
