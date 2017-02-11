using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestTopCommunity : RestEntity<string>
    {
        [JsonProperty("avatar_image_url")]
        public string AvatarUrl { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("channels")]
        public uint ChannelTotal { get; internal set; }
        [JsonProperty("viewers")]
        public uint ViewerTotal { get; internal set; }

        internal RestTopCommunity(BaseRestClient client) : base(client) { }

        internal static RestTopCommunity Create(BaseRestClient client, string json)
        {
            var community = new RestTopCommunity(client);
            JsonConvert.PopulateObject(json, community);
            return community;
        }
    }
}
