using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestUser : RestUserSummary
    {
        [TwitchJsonProperty("bio")]
        public string Bio { get; internal set; }
        [TwitchJsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [TwitchJsonProperty("type")]
        public string Type { get; internal set; }
        [TwitchJsonProperty("updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        internal RestUser(BaseRestClient client) : base(client) { }

        internal static new RestUser Create(BaseRestClient client, string json)
        {
            var user = new RestUser(client);
            JsonConvert.PopulateObject(json, user);
            return user;
        }
    }
}
