namespace NTwitch.Rest
{
    public class ChannelProperties
    {
        public Optional<string> Status { get; set; }
        public Optional<string> Game { get; set; }
        public Optional<string> Delay { get; set; }
        public Optional<bool> IsFeedEnabled { get; set; }
    }
}
