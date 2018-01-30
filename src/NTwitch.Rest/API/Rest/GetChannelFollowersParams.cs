namespace NTwitch.Rest
{
    internal class GetChannelFollowersParams
    {
        public Optional<bool> IsAscending { get; set; }
        public Optional<int> Limit { get; set; }
        public Optional<int> Offset { get; set; }
    }
}
