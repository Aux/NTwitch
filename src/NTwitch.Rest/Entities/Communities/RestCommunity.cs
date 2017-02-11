using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestCommunity : RestEntity<string>
    {
        [JsonProperty("avatar_image_url")]
        public string AvatarUrl { get; internal set; }
        [JsonProperty("cover_image_url")]
        public string CoverUrl { get; internal set; }
        [JsonProperty("description")]
        public string Description { get; internal set; }
        [JsonProperty("decription_html")]
        public string DescriptionRaw { get; internal set; }
        [JsonProperty("language")]
        public string Language { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("owner_id")]
        public ulong OwnerId { get; internal set; }
        [JsonProperty("rules")]
        public string Rules { get; internal set; }
        [JsonProperty("rules_html")]
        public string RulesRaw { get; internal set; }
        [JsonProperty("summary")]
        public string Summary { get; internal set; }

        internal RestCommunity(BaseRestClient client) : base(client) { }

        internal static RestCommunity Create(BaseRestClient client, string json)
        {
            var community = new RestCommunity(client);
            JsonConvert.PopulateObject(json, community);
            return community;
        }

        public Task ModifyAsync(Action<ModifyCommunityParams> action)
            => throw new NotImplementedException();

        public Task GetBannedUsersAsync()
            => throw new NotImplementedException();
        
        public Task BanUserAsync()
            => throw new NotImplementedException();

        public Task UnbanUserAsync()
            => throw new NotImplementedException();
    }
}
