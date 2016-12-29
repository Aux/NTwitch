namespace NTwitch.Rest
{
    public class RestGame : RestEntity, IGame
    {
        public TwitchImage Box { get; }
        public ulong GiantbombId { get; }
        public TwitchImage Logo { get; }
        public string Name { get; }
        public int Popularity { get; }

        public RestGame(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
