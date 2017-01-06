using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestFollow : IEntity, IFollow
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("")]
        public bool IsNotificationEnabled { get; internal set; }

        internal RestFollow(TwitchRestClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
