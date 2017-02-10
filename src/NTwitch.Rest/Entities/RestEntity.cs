using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestEntity : IEntity
    {
        [JsonIgnore]
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }

        internal RestEntity(BaseRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
