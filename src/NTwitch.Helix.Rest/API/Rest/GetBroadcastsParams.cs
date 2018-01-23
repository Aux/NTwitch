namespace NTwitch.Helix.API
{
    internal class GetBroadcastsParams
    {
        public Optional<ulong[]> CommunityIds { get; set; }
        public Optional<string[]> Languages { get; set; }
        public Optional<ulong[]> UserIds { get; set; }
        public Optional<string[]> UserNames { get; set; }
        public Optional<BroadcastType> Type { get; set; }

        // Pagination
        public Optional<string> After { get; set; }
        public Optional<string> Before { get; set; }
        public Optional<int> First { get; set; }
    }
}
