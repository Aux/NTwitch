using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class TwitchChatClientConfig
    {
        public string Host { get; } = "irc.chat.twitch.tv";
        public int Port { get; } = 6667;
    }
}
