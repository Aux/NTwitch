using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Pubsub
{
    internal class SocketData
    {
        [JsonProperty("topics")]
        public IEnumerable<string> Topics { get; set; }
        [JsonProperty("auth_token")]
        public string Token { get; set; }
    }
}
