using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class Ingest
    {
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("availability")]
        public double Availability { get; set; }
        [JsonProperty("default")]
        public bool IsDefault { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url_template")]
        public string UrlTemplate { get; set; }
    }
}
