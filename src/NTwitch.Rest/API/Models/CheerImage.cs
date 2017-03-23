using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class CheerImage
    {
        [JsonProperty("dark")]
        public CheerScale Dark { get; set; }
        [JsonProperty("light")]
        public CheerScale Light { get; set; }
    }
}
