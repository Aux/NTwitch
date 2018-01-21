using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class StreamCollection
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("stream")]
        public Broadcast Stream { get; set; }
        [JsonProperty("streams")]
        public IEnumerable<Broadcast> Streams { get; set; }
        [JsonProperty("featured")]
        public IEnumerable<FeaturedStream> Featured { get; set; }
    }
}
