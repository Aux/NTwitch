using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class Game
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("box_art_url")]
        public string BoxArtUrl { get; set; }
    }
}
