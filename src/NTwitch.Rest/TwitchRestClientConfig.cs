using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class TwitchRestClientConfig
    {
        public string BaseUrl { get; set; } = "https://api.twitch.tv/kraken/";
        public LogLevel LogLevel { get; set; } = LogLevel.Error;
    }
}
