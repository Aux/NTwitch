using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestStreamSummary : IEntity, IStreamSummary
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public int Channels { get; internal set; }
        [JsonProperty("")]
        public int Viewers { get; internal set; }

        internal RestStreamSummary(TwitchRestClient client)
        {
            Client = client as TwitchRestClient;
        }

        internal static RestStreamSummary Create(BaseTwitchClient client, string json)
        {
            var stream = new RestStreamSummary(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, stream);
            return stream;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
