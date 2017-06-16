using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    internal class Message
    {



        // Subscription
        [JsonProperty("message")]
        public string Content { get; set; }
        [JsonProperty("emotes")]
        public IEnumerable<Emote> Emotes { get; set; }
    }
}
