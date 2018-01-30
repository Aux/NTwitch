namespace NTwitch.Rest
{
    internal class GetFeaturedBroadcastsParams
    {
        public Optional<int> Limit { get; set; }
        public Optional<int> Offset { get; set; }
    }
}
