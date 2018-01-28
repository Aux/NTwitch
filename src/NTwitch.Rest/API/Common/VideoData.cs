using System.Collections.Generic;
using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class VideoData
    {
        [JsonProperty("vods")]
        public Optional<IEnumerable<Video>> Vods { get; set; }
        [JsonProperty("videos")]
        public Optional<IEnumerable<Video>> Videos { get; set; }
    }
}
