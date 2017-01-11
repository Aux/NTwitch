using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class TwitchChatConfig : TwitchRestConfig
    {
        public string ChatUrl { get; set; } = "irc.chat.twitch.tv";
        public int ChatPort { get; set; } = 6667;
    }
}
