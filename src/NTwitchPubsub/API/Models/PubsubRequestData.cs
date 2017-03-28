using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    internal class PubsubRequestData
    {
        [JsonProperty("topics")]
        public List<string> Topics { get; set; } = new List<string>();
        [JsonProperty("auth_token")]
        public string Token { get; set; } = null;
    }
}
