namespace NTwitch.Rest
{
    public class TwitchRestConfig
    {
        public string ApiUrl { get; set; } = "https://api.twitch.tv/kraken/";
        public LogLevel LogLevel { get; set; } = LogLevel.Error;
    }
}
