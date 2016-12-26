using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.WebSocket
{
    public class TwitchSocketConfig
    {
        public string SocketUrl { get; set; } = "wss://pubsub-edge.twitch.tv";
        public LogLevel LogLevel { get; set; } = LogLevel.Error;
    }
}
