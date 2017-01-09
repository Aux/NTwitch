using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatChannel : ChatEntity
    {
        public string Name { get; private set; }
        
        public ChatChannel(TwitchChatClient client) : base(client) { }

        public Task JoinAsync()
        {
            throw new NotImplementedException();
        }

        public Task LeaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task SendMessageAsync(string content)
        {
            throw new NotImplementedException();
        }
    }
}
