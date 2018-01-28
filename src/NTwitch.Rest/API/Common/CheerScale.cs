using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class CheerScale
    {
        [JsonProperty("animated")]
        public IReadOnlyDictionary<double, string> Animated { get; set; }
        [JsonProperty("static")]
        public IReadOnlyDictionary<double, string> Static { get; set; }
    }
}
