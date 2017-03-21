using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class PreTopVideo
    {
        [JsonProperty("vods")]
        public IEnumerable<Video> Videos { get; set; }
    }
}
