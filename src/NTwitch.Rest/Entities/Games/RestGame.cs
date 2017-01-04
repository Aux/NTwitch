using System.Runtime.CompilerServices;


namespace NTwitch.Rest
{
    public class RestGame : IEntity, IGame
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public TwitchImage Box { get; internal set; }
        public ulong GiantbombId { get; internal set; }
        public TwitchImage Logo { get; internal set; }
        public string Name { get; internal set; }
        public int Popularity { get; internal set; }

        internal RestGame(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
