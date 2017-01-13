using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestEmoteImage : EmoteBase
    {
        [JsonProperty("")]
        public int Height { get; private set; }
        [JsonProperty("")]
        public string ImageUrl { get; private set; }
        [JsonProperty("")]
        public ulong SetId { get; private set; }
        [JsonProperty("")]
        public int Width { get; private set; }

        public RestEmoteImage(TwitchRestClient client) : base(client) { }

        public static RestEmoteImage Create(BaseRestClient client, string json)
        {
            var emote = new RestEmoteImage(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, emote);
            return emote;
        }
    }
}
