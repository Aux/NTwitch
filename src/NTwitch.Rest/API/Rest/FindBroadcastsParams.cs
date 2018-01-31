namespace NTwitch.Rest
{
    internal class FindBroadcastsParams
    {
        public Optional<string> Query { get; set; }
        public Optional<bool?> IsHLS { get; set; }
        public Optional<int> Limit { get; set; }
        public Optional<int> Offset { get; set; }
    }
}
