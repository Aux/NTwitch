using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class OverwatchMetadata
    {
        [JsonProperty("broadcaster")]
        public OverwatchPlayer Broadcaster { get; set; }
    }
}
