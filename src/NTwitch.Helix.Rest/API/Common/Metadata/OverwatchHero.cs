using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class OverwatchHero
    {
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ability")]
        public string Ability { get; set; }
    }
}
