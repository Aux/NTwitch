using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class HearthstoneMetadata
    {
        [JsonProperty("broadcaster")]
        public HearthstonePlayer Broadcaster { get; set; }
        [JsonProperty("opponent")]
        public HearthstonePlayer Opponent { get; set; }
    }
}
