using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatUserMessage : ChatMessage
    {
        public ChatChannel Channel { get; private set; }
        public ChatUser User { get; private set; }
    }
}
