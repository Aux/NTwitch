using Newtonsoft.Json;

namespace NTwitch
{
    public class EmoticonImage
    {
        [JsonProperty("emoticon_set")]
        public int SetId { get; }
        [JsonProperty("height")]
        public int Height { get; }
        [JsonProperty("width")]
        public int Width { get; }
        [JsonProperty("url")]
        public int ImageUrl { get; }
    }
}
