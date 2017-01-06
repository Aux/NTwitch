using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestGame : IEntity, IGame
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public TwitchImage Box { get; internal set; }
        [JsonProperty("")]
        public ulong GiantbombId { get; internal set; }
        [JsonProperty("")]
        public TwitchImage Logo { get; internal set; }
        [JsonProperty("")]
        public string Name { get; internal set; }
        [JsonProperty("")]
        public int Popularity { get; internal set; }

        internal RestGame(TwitchRestClient client)
        {
            Client = client;
        }

        internal static RestGame Create(BaseTwitchClient client, string json)
        {
            var game = new RestGame(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, game);
            return game;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
