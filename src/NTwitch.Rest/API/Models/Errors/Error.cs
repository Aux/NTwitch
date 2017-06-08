using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal abstract class Error
    {
        [JsonProperty("error")]
        public Optional<string> ErrorText { get; set; }
        [JsonProperty("status")]
        public Optional<int> ErrorStatus { get; set; }
        [JsonProperty("message")]
        public Optional<ErrorMessage> ErrorMessage { get; set; }
    }
}
