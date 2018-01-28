using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class ClipData
    {
        [JsonProperty("cursor")]
        public string Cursor { get; set; }
        [JsonProperty("clips")]
        public IReadOnlyCollection<Clip> Clips { get; set; }
    }
}
