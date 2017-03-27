using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class Game
    {
        [JsonProperty("_id")]
        public ulong Id { get; set; }
        [JsonProperty("box")]
        public Dictionary<string, string> Box { get; set; }
        [JsonProperty("giantbomb_id")]
        public ulong GiantbombId { get; set; }
        [JsonProperty("logo")]
        public Dictionary<string, string> Logo { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("popularity")]
        public uint Viewers { get; set; }
    }
}
