using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestTopGame : IEntity, ITopGame
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public int Channels { get; internal set; }
        [JsonProperty("")]
        public RestGame Game { get; internal set; }
        [JsonProperty("")]
        public int Viewers { get; internal set; }

        internal RestTopGame(TwitchRestClient client)
        {
            Client = client;
        }

        internal static RestTopGame Create(BaseTwitchClient client, string json)
        {
            var game = new RestTopGame(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, game);
            return game;
        }

        ITwitchClient IEntity.Client
            => Client;
        IGame ITopGame.Game
            => Game;
    }
}
