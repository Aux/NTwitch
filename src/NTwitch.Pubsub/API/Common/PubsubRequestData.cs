using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Pubsub
{
    internal class PubsubRequestData
    {
        [JsonProperty("topics")]
        public List<string> Topics { get; set; }
        [JsonProperty("auth_token")]
        public string Token { get; set; }
    }
}
