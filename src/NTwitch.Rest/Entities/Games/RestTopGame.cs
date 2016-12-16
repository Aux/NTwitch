namespace NTwitch.Rest
{
    public class RestTopGame : ITopGame
    {
        public uint Id { get; }
        public uint GiantBombId { get; }
        public string Name { get; }
        public TwitchImage BoxArt { get; }
        public TwitchImage Logo { get; }
        public int Channels { get; }
        public int Viewers { get; }
    }
}
