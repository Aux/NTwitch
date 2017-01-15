namespace NTwitch.Rest
{
    public class TwitchRestConfig
    {
        public string RestUrl { get; set; } = "https://api.twitch.tv/kraken/";
        public LogLevel LogLevel { get; set; } = LogLevel.Info;
    }
}
