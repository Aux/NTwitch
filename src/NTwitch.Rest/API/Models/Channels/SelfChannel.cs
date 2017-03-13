using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class SelfChannel : Channel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("stream_key")]
        public string StreamKey { get; set; }
    }
}
