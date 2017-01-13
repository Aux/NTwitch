using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestIngest : IngestBase
    {
        [JsonProperty("")]
        public double Availability { get; private set; }
        [JsonProperty("")]
        public bool IsDefault { get; private set; }
        [JsonProperty("")]
        public string Name { get; private set; }
        [JsonProperty("")]
        public string UrlTemplate { get; private set; }

        internal RestIngest(BaseRestClient client) : base(client) { }

        internal static RestIngest Create(BaseRestClient client, string json)
        {
            var ingest = new RestIngest(client);
            JsonConvert.PopulateObject(json, ingest);
            return ingest;
        }
    }
}
