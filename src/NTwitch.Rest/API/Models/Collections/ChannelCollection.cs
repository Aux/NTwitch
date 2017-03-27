using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class ChannelCollection
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("channels")]
        public IEnumerable<Channel> Channels { get; set; }
    }
}
