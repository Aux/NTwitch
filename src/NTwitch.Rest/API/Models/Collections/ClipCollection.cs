using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class ClipCollection
    {
        [JsonProperty("cursor")]
        public string Cursor { get; set; }
        [JsonProperty("clips")]
        public IEnumerable<Clip> Clips { get; set; }
    }
}
