using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestUser : BaseUser
    {
        [JsonProperty("bio")]
        public string Bio { get; internal set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; internal set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        internal RestUser(TwitchRestClient client) : base(client) { }

        public static RestUser Create(BaseRestClient client, string json)
        {
            var user = new RestUser(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, user);
            return user;
        }
    }
}
