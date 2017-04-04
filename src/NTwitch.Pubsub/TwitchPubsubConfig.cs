using NTwitch.Rest;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubConfig : TwitchRestConfig
    {
        /// <summary> Custom client provider for pubsub requests </summary>
        public SocketClient PubsubProvider { get; set; } = null;
        /// <summary> The host to connect to when making pubsub requests </summary>
        public string PubsubHost { get; set; } = "wss://pubsub-edge.twitch.tv";
    }
}
