using NTwitch.Rest;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubConfig : TwitchRestConfig
    {
        /// <summary> Gets or sets the provider used to generate new websocket connections. </summary>
        public SocketClientProvider WebSocketProvider { get; set; } = DefaultWebSocketProvider.Instance;
        /// <summary> The host to connect to when making pubsub requests </summary>
        public string WebSocketHost { get; set; } = "wss://pubsub-edge.twitch.tv";

        /// <summary> Gets or sets the time, in milliseconds, to wait for a connection to complete before aborting. </summary>
        public int ConnectionTimeout { get; set; } = 30000;
        /// <summary> Gets or sets the time, in milliseconds, between heartbeat requests. </summary>
        public int HeartbeatInterval { get; set; } = 300000;
    }
}
