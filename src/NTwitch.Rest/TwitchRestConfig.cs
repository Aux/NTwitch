namespace NTwitch.Rest
{
    public class TwitchRestConfig : TwitchConfig
    {
        public string RestHost { get; set; } = "https://api.twitch.tv/kraken/";
    }
}
