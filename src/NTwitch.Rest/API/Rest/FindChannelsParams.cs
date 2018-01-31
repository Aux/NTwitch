namespace NTwitch.Rest
{
    internal class FindChannelsParams
    {
        public Optional<string> Query { get; set; }
        public Optional<int> Limit { get; set; }
        public Optional<int> Offset { get; set; }
    }
}
