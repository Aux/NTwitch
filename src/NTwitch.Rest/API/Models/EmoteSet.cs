using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class EmoteSet
    {
        [JsonProperty("emoticon_sets")]
        public Dictionary<string, IEnumerable<Emote>> Emotes { get; set; }
    }
}
