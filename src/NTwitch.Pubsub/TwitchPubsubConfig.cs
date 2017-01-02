using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubConfig
    {
        public string SocketUrl { get; set; } = "wss://pubsub-edge.twitch.tv";
        public bool PopulateOptionalEventData { get; set; } = true;
        public LogLevel LogLevel { get; set; } = LogLevel.Error;
    }
}
