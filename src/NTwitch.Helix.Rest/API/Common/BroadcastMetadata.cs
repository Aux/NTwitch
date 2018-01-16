using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class BroadcastMetadata
    {
        [JsonProperty("user_id")]
        public ulong UserId { get; set; }
        [JsonProperty("game_id")]
        public ulong GameId { get; set; }
        [JsonProperty("overwatch")]
        public Optional<OverwatchMetadata> Overwatch { get; set; }
        [JsonProperty("hearthstone")]
        public Optional<HearthstoneMetadata> Hearthstone { get; set; }
    }
}
