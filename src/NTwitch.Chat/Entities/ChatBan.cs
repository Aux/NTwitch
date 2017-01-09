using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatBan : ChatEntity
    {
        public DateTime Timestamp { get; private set; }
        public ChatChannel Channel { get; private set; }
        public ChatUser User { get; private set; }
        public string Reason { get; private set; }

        public ChatBan(TwitchChatClient client) : base(client) { }
    }
}
