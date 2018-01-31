namespace NTwitch.Rest
{
    internal class FindGamesParams
    {
        public Optional<string> Query { get; set; }
        public Optional<bool> IsLive { get; set; }
        public Optional<int> Limit { get; set; }
        public Optional<int> Offset { get; set; }
    }
}
