namespace NTwitch.Rest
{
    public class TwitchRestConfig
    {
        public string RestUrl { get; set; }
        public LogLevel LogLevel { get; set; } = LogLevel.Info;
    }
}
