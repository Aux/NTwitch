namespace NTwitch.Rest
{
    public class TwitchRestConfig : TwitchConfig
    {
        /// <summary> The client id of the application, required when not authenticated or authenticated as multiple users. </summary>
        public string ClientId { get; set; }
        /// <summary> The base url used to make rest requests </summary>
        public string RestHost { get; set; } = "https://api.twitch.tv/kraken/";
    }
}
