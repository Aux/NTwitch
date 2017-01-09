using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class TwitchChatConfig : TwitchRestConfig
    {
        public string IrcUrl { get; set; } = "";
        public int Port { get; set; } = 0;
    }
}
