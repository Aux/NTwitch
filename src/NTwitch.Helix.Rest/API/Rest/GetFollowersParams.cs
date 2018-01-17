namespace NTwitch.Helix.API
{
    internal class GetFollowersParams
    {
        public Optional<ulong> FromId { get; set; }
        public Optional<ulong> ToId { get; set; }
        public Optional<string> After { get; set; }
        public Optional<string> Before { get; set; }
        public Optional<int> First { get; set; }
    }
}
