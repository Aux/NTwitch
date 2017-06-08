using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class VideoThumbnailImage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
