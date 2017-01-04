
using System.Runtime.CompilerServices;


namespace NTwitch.Rest
{
    public class RestTopGame : IEntity, ITopGame
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public int Channels { get; internal set; }
        public RestGame Game { get; internal set; }
        public int Viewers { get; internal set; }

        internal RestTopGame(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
        IGame ITopGame.Game
            => Game;
    }
}
