namespace Twitch.Rest
{
    public class RestGame : IGame
    {
        public uint Id { get; }
        public uint GiantBombId { get; }
        public string Name { get; }
        public TwitchImage BoxArt { get; }
        public TwitchImage Logo { get; }
    }
}
