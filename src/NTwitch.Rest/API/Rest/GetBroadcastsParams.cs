namespace NTwitch.Rest
{
    internal class GetBroadcastsParams
    {
        public Optional<ulong[]> ChannelIds { get; set; }
        public Optional<string> Game { get; set; }
        public Optional<string> Language { get; set; }
        public Optional<StreamType> Type { get; set; }
        public Optional<int> Limit { get; set; }
        public Optional<int> Offset { get; set; }
    }
}
