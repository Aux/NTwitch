using NTwitch.Rest;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubConfig : TwitchRestConfig
    {
        public string PubsubHost { get; set; } = "wss://pubsub-edge.twitch.tv";
        public string ChatHost { get; set; } = "wss://irc-ws.chat.twitch.tv";
    }
}
