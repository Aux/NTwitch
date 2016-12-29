namespace NTwitch.Rest
{
    public class RestTopGame : RestEntity, ITopGame
    {
        public int Channels { get; }
        public RestGame Game { get; }
        public int Viewers { get; }

        public RestTopGame(TwitchRestClient client, ulong id) : base(client, id) { }

        //IGame
        IGame ITopGame.Game
            => Game;
    }
}
