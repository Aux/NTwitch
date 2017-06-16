using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class Error
    {
        [JsonProperty("error")]
        public string ErrorText { get; set; }
        [JsonProperty("status")]
        public int? ErrorStatus { get; set; }
        [JsonProperty("message")]
        public string ErrorMessage { get; set; }

        public ErrorMessage GetErrorMessage()
            => JsonConvert.DeserializeObject<ErrorMessage>(ErrorMessage);
    }
}
