using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class PreCheer
    {
        [JsonProperty("actions")]
        public IEnumerable<CheerInfo> Actions { get; set; }
    }
}
