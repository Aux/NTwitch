using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestGame : RestEntity
    {
        [JsonProperty("")]
        public TwitchImage Box { get; private set; }
        [JsonProperty("")]
        public ulong GiantbombId { get; private set; }
        [JsonProperty("")]
        public TwitchImage Logo { get; private set; }
        [JsonProperty("")]
        public string Name { get; private set; }
        [JsonProperty("")]
        public int Popularity { get; private set; }

        public RestGame(BaseRestClient client) : base(client) { }

        public static RestGame Create(BaseRestClient client, string json)
        {
            var game = new RestGame(client);
            JsonConvert.PopulateObject(json, game);
            return game;
        }
    }
}
