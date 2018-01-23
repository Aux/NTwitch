namespace NTwitch.Helix.API
{
    internal class GetVideosParams
    {
        public Optional<ulong[]> VideoIds { get; set; }
        public Optional<ulong> UserId { get; set; }
        public Optional<ulong> GameId { get; set; }
        public Optional<string> Language { get; set; }
        public Optional<VideoPeriod> Period { get; set; }
        public Optional<VideoSort> Sort { get; set; }
        public Optional<VideoType> Type { get; set; }
        
        // Pagination
        public Optional<string> After { get; set; }
        public Optional<string> Before { get; set; }
        public Optional<int> First { get; set; }
    }
}
