using Newtonsoft.Json;

namespace NTwitch
{
    public class TwitchCommercial
    {
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("retryafter")]
        public int RetryAfter { get; set; }
    }
}
