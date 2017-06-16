using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class Community
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("owner_id")]
        public ulong OwnerId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }
        [JsonProperty("rules")]
        public string Rules { get; set; }
        [JsonProperty("rules_html")]
        public string RulesHtml { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("avatar_image_url")]
        public string AvatarImageUrl { get; set; }
        [JsonProperty("cover_image_url")]
        public string CoverImageUrl { get; set; }
        [JsonProperty("channels")]
        public uint Channels { get; set; }
        [JsonProperty("viewers")]
        public uint Viewers { get; set; }
    }
}
