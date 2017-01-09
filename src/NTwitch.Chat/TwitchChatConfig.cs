using NTwitch.Rest;
using System.Text;

namespace NTwitch.Chat
{
    public class TwitchChatConfig : TwitchRestConfig
    {
        public string IrcUrl { get; set; } = "irc.chat.twitch.tv.";
        public int Port { get; set; } = 6667;
        bool CanSpeakWithoutMod { get; set; } = true;
        //Encoding TextEncoding { get; set; }
    }
}
