using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestBlockedUser
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; private set; }
        [JsonProperty("")]
        public DateTime UpdatedAt { get; private set; }
        [JsonProperty("")]
        public RestUser User { get; private set; }

        internal RestBlockedUser(TwitchRestClient client)
        {
            Client = client;
        }

        internal static RestBlockedUser Create(BaseRestClient client, string json)
        {
            var user = new RestBlockedUser(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, user);
            return user;
        }
    }
}
