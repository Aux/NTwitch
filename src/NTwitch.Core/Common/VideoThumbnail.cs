using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch
{
    public class VideoThumbnail
    {
        [JsonProperty("small")]
        public IEnumerable<VideoThumbnailImage> Small { get; internal set; }
        [JsonProperty("medium")]
        public IEnumerable<VideoThumbnailImage> Medium { get; internal set; }
        [JsonProperty("large")]
        public IEnumerable<VideoThumbnailImage> Large { get; internal set; }
        [JsonProperty("template")]
        public IEnumerable<VideoThumbnailImage> Template { get; internal set; }
    }
}
