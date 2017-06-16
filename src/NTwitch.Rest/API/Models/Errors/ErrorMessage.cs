using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class ErrorMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("error")]
        public string Content { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
