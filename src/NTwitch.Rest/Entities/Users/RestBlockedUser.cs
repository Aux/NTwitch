using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestBlockedUser
    {
        public BaseRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; private set; }
        [JsonProperty("")]
        public DateTime UpdatedAt { get; private set; }
        [JsonProperty("")]
        public RestUser User { get; private set; }

        internal RestBlockedUser(BaseRestClient client)
        {
            Client = client;
        }

        internal static RestBlockedUser Create(BaseRestClient client, string json)
        {
            var user = new RestBlockedUser(client);
            JsonConvert.PopulateObject(json, user);
            return user;
        }
    }
}
