using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Pubsub
{
    public class PubsubRequestData
    {
        [JsonProperty("topics")]
        public List<string> Topics { get; private set; } = new List<string>();
        [JsonProperty("auth_token")]
        public string Token { get; private set; } = null;

        public PubsubRequestData(string[] topics, string token)
        {
            Topics.AddRange(topics);
            Token = token;
        }
    }
}
