using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestIngest : RestEntity<ulong>
    {
        [JsonProperty("availability")]
        public double Availability { get; internal set; }
        [JsonProperty("default")]
        public bool IsDefault { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("url_template")]
        public string UrlTemplate { get; internal set; }

        public RestIngest(BaseRestClient client) : base(client) { }
    }
}
