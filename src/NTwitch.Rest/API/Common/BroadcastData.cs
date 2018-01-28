using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class BroadcastData
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("stream")]
        public Broadcast Broadcast { get; set; }
        
        [JsonProperty("streams")]
        public Optional<IReadOnlyCollection<Broadcast>> Broadcasts { get; set; }
        [JsonProperty("featured")]
        public Optional<IReadOnlyCollection<FeaturedBroadcast>> Featured { get; set; }
    }
}
