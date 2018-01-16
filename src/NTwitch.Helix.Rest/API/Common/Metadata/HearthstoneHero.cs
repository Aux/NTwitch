using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class HearthstoneHero
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("class")]
        public string Class { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
