using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    internal class PubsubOutData
    {
        [JsonProperty("topics")]
        public List<string> Topics { get; set; }
        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }
    }
}
