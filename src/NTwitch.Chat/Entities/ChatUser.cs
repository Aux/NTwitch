using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatUser : ChatEntity
    {
        public string Name { get; private set; }

        public ChatUser(TwitchChatClient client) : base(client) { }

        public Task BanAsync(string channel, int? duration = null)
        {
            throw new NotImplementedException();
        }

        public Task BanAsync(ChatChannel channel, int? duration = null)
        {
            throw new NotImplementedException();
        }
    }
}
