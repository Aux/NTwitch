namespace NTwitch.Rest
{
    internal class GetFollowedBroadcastsParams
    {
        public Optional<BroadcastType> Type { get; set; }
        public Optional<int> Limit { get; set; }
        public Optional<int> Offset { get; set; }
    }
}
