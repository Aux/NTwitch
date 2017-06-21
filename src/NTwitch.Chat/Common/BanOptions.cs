namespace NTwitch.Chat
{
    public class BanOptions
    {
        /// <summary> The reason provided for this ban </summary>
        public string Reason { get; }
        /// <summary> The duration in seconds of this ban </summary>
        public long? Duration { get; }

        public BanOptions(string reason, long? duration = null)
        {
            Reason = reason;
            Duration = duration;
        }
    }
}
