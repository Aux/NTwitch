using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class BlockCollection
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("blocks")]
        public IEnumerable<BlockedUser> Blocks { get; set; }
    }
}
