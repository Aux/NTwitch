using NTwitch.Rest;

namespace NTwitch.WebSocket
{
    public class TwitchSocketConfig : TwitchRestConfig
    {
        public string PubsubHost { get; set; } = "wss://pubsub-edge.twitch.tv";
        public string ChatHost { get; set; } = "wss://irc-ws.chat.twitch.tv";
        public ISocketCache CacheInstance { get; set; } = new SocketCache();
    }
}
