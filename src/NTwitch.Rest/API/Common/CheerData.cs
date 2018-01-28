using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class CheerData
    {
        [JsonProperty("actions")]
        public IReadOnlyCollection<CheerInfo> Actions { get; set; }
    }
}
