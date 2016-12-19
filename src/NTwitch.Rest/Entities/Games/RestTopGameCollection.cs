using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestTopGameCollection
    {
        [JsonProperty("top")]
        public IEnumerable<RestTopGame> Games { get; internal set; }
        [JsonProperty("_links")]
        public TwitchLinks Links { get; internal set; }
        [JsonProperty("_total")]
        public int Count { get; internal set; }
    }
}
