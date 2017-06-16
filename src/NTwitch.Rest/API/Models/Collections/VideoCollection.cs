using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class VideoCollection
    {
        [JsonProperty("vods")]
        public IEnumerable<Video> Vods { get; set; }
        [JsonProperty("videos")]
        public IEnumerable<Video> Videos { get; set; }
    }
}
