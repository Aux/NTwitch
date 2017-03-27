namespace NTwitch.Rest
{
    public class TwitchRestConfig : TwitchConfig
    {
        public string RestHost { get; set; } = "https://api.twitch.tv/kraken/";
        //public ICache CacheProvider { get; set; } = new RestCache();
        //public CacheExpireMode CacheMode { get; set; } = CacheExpireMode.Limit;
        //public int CacheLimit { get; set; } = 100;
    }
}
