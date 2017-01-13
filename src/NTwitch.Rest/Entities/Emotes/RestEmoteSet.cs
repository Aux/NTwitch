using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestEmoteSet : EmoteBase
    {
        [JsonProperty("")]
        public IEnumerable<RestEmote> Emotes { get; internal set; }

        public RestEmoteSet(TwitchRestClient client) : base(client) { }

        public static RestEmoteSet Create(BaseRestClient client, string json)
        {
            var emote = new RestEmoteSet(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, emote);
            return emote;
        }
    }
}
