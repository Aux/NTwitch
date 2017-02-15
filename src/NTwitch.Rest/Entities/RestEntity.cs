using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestEntity<T> : IEntity<T>
    {
        [JsonIgnore]
        internal BaseRestClient Client { get; }
        [JsonProperty("_id"), JsonPropertyAlias("id", "user_id")]
        public T Id { get; internal set; }

        internal RestEntity(BaseRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity<T>.Client
            => Client;
    }
}
