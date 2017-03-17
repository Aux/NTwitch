using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    public class PreCheer
    {
        [JsonProperty("actions")]
        public IEnumerable<CheerInfo> Actions { get; set; }
    }
}
