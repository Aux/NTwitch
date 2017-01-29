using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestIngest : RestEntity
    {
        [JsonProperty("availability")]
        public double Availability { get; private set; }
        [JsonProperty("default")]
        public bool IsDefault { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("url_template")]
        public string UrlTemplate { get; private set; }

        public RestIngest(BaseRestClient client) : base(client) { }
    }
}
