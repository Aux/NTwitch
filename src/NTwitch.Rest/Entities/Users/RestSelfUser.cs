using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestSelfUser : SelfUserBase
    {
        [JsonProperty("bio")]
        public string Bio { get; private set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; private set; }
        [JsonProperty("email")]
        public string Email { get; private set; }
        [JsonProperty("partnered")]
        public bool IsPartnered { get; private set; }
        [JsonProperty("twitter_connected")]
        public bool IsTwitterConnected { get; private set; }
        [JsonProperty("email_verified")]
        public bool IsVerified { get; private set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("notifications")]
        public TwitchNotifications Notifications { get; private set; }
        [JsonProperty("type")]
        public string Type { get; private set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; private set; }

        internal RestSelfUser(BaseRestClient client) : base(client) { }

        internal static RestSelfUser Create(BaseRestClient client, string json)
        {
            var user = new RestSelfUser(client);
            JsonConvert.PopulateObject(json, user);
            return user;
        }
    }
}
