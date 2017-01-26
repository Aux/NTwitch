namespace NTwitch.Rest
{
    public class TwitchRestConfig : TwitchConfig
    {
        public string RestUrl { get; set; } = "https://api.twitch.tv/kraken/";
    }
}
