using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class VideoThumbnail
    {
        [JsonProperty("small")]
        public IEnumerable<VideoThumbnailImage> Small { get; set; }
        [JsonProperty("medium")]
        public IEnumerable<VideoThumbnailImage> Medium { get; set; }
        [JsonProperty("large")]
        public IEnumerable<VideoThumbnailImage> Large { get; set; }
        [JsonProperty("template")]
        public IEnumerable<VideoThumbnailImage> Template { get; set; }
    }
}
