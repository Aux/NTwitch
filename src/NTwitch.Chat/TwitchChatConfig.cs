namespace NTwitch.Chat
{
    public class TwitchChatConfig
    {
        public string Host { get; set; } = "irc.chat.twitch.tv";
        public int Port { get; set; } = 6667;
        public LogLevel LogLevel { get; set; }
    }
}
