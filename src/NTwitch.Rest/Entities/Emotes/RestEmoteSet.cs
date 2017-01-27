using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestEmoteSet : RestEntity
    {
        [JsonProperty("")]
        public IEnumerable<RestEmote> Emotes { get; internal set; }

        public RestEmoteSet(BaseRestClient client) : base(client) { }

        public static RestEmoteSet Create(BaseRestClient client, string json)
        {
            var emote = new RestEmoteSet(client);
            JsonConvert.PopulateObject(json, emote);
            return emote;
        }
    }
}
