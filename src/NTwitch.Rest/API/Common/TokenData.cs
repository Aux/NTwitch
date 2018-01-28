using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class TokenData
    {
        [JsonProperty("token")]
        public Token Token { get; set; }
    }
}
