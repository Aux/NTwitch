using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestCommunityUser
    {
        [JsonProperty("user_id")]
        public ulong Id { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("avatar_image_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("start_timestamp")]
        public DateTime BannedAt { get; set; }
        
        internal RestCommunityUser(BaseRestClient client) /*: base(client)*/ { }

        internal static RestCommunityUser Create(BaseRestClient client, string json)
        {
            var user = new RestCommunityUser(client);
            JsonConvert.PopulateObject(json, user);
            return user;
        }
    }
}
