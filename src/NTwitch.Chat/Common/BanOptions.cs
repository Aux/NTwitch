namespace NTwitch.Chat
{
    public class BanOptions
    {
        public string Reason { get; }
        public long? Duration { get; }

        public BanOptions(string reason, long? duration = null)
        {
            Reason = reason;
            Duration = duration;
        }
    }
}
