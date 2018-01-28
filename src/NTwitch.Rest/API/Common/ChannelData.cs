using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class ChannelData
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("channels")]
        public IReadOnlyCollection<Channel> Channels { get; set; }
    }
}
