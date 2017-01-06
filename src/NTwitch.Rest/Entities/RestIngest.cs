using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestIngest : IEntity, IIngest
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public double Availability { get; internal set; }
        [JsonProperty("")]
        public bool IsDefault { get; internal set; }
        [JsonProperty("")]
        public string Name { get; internal set; }
        [JsonProperty("")]
        public string UrlTemplate { get; internal set; }

        internal RestIngest(TwitchRestClient client)
        {
            Client = client;
        }

        internal static RestIngest Create(BaseTwitchClient client, string json)
        {
            var ingest = new RestIngest(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, ingest);
            return ingest;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
