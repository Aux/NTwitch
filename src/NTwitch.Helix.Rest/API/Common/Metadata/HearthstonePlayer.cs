using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class HearthstonePlayer
    {
        [JsonProperty("hero")]
        public HearthstoneHero Hero { get; set; }
    }
}
