using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class TokenCollection
    {
        [JsonProperty("token")]
        public Token Token { get; set; }
    }
}
