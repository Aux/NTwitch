using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestSubscription : IEntity, ISubscription
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; internal set; }

        internal RestSubscription(TwitchRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
