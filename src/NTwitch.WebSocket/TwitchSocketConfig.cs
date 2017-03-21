using NTwitch.Rest;

namespace NTwitch.WebSocket
{
    public class TwitchSocketConfig : TwitchRestConfig
    {
        public string SocketHost { get; set; } = "wss://Socket-edge.twitch.tv";
    }
}
