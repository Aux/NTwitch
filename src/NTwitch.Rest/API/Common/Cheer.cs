using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class Cheer
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("images")]
        public IReadOnlyCollection<CheerImage> Images { get; set; }
        [JsonProperty("min_bits")]
        public int MinimumBits { get; set; }
    }
}
