using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class StreamCollection
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("stream")]
        public Stream Stream { get; set; }
        [JsonProperty("streams")]
        public IEnumerable<Stream> Streams { get; set; }
        [JsonProperty("featured")]
        public IEnumerable<FeaturedStream> Featured { get; set; }
    }
}
