using System;
using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestEntity<T> : IEntity<T>
    {
        [JsonIgnore]
        internal BaseRestClient Client { get; }
        [TwitchJsonProperty("_id")]
        public T Id { get; internal set; }

        internal RestEntity(BaseRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity<T>.Client
            => Client;
    }
}
