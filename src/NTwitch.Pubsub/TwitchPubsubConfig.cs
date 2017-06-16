using NTwitch.Rest;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubConfig : TwitchRestConfig
    {
        /// <summary> Gets or sets the provider used to generate new websocket connections. </summary>
        public SocketClientProvider WebSocketProvider { get; set; } = DefaultWebSocketProvider.Instance;
        /// <summary> The host to connect to when making pubsub requests </summary>
        public string WebSocketHost { get; set; } = "wss://pubsub-edge.twitch.tv";
    }
}
