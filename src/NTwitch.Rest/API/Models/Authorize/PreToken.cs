using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class PreToken
    {
        [JsonProperty("token")]
        public Token Token { get; set; }
    }
}
