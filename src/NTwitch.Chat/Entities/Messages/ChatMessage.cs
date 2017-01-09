using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatMessage : ChatEntity
    {
        public DateTime Timestamp { get; private set; }
        public string Content { get; private set; }
        
        public ChatMessage(TwitchChatClient client) : base(client) { }
    }
}
