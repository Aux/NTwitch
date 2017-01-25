using Newtonsoft.Json;

namespace NTwitch
{
    public class CreatePostParams
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("share")]
        public bool Share { get; set; }
    }
}
