using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class OverwatchPlayer
    {
        [JsonProperty("hero")]
        public OverwatchHero Hero { get; set; }
    }
}
