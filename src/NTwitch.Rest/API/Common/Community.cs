using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class Community
    {
        // Simple Community
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("avatar_image_url")]
        public string AvatarImageUrl { get; set; }

        // Community
        [JsonProperty("owner_id")]
        public Optional<ulong> OwnerId { get; set; }
        [JsonProperty("summary")]
        public Optional<string> Summary { get; set; }
        [JsonProperty("description")]
        public Optional<string> Description { get; set; }
        [JsonProperty("description_html")]
        public Optional<string> DescriptionHtml { get; set; }
        [JsonProperty("rules")]
        public Optional<string> Rules { get; set; }
        [JsonProperty("rules_html")]
        public Optional<string> RulesHtml { get; set; }
        [JsonProperty("language")]
        public Optional<string> Language { get; set; }
        [JsonProperty("cover_image_url")]
        public Optional<string> CoverImageUrl { get; set; }
        [JsonProperty("channels")]
        public Optional<uint> Channels { get; set; }
        [JsonProperty("viewers")]
        public Optional<uint> Viewers { get; set; }
    }
}
