using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class VideoThumbnail
    {
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("url")]
        public string Url { get; internal set; }
    }
}
