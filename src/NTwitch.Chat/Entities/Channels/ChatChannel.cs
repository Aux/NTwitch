using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatChannel : ChannelBase
    {
        public ChatChannel(TwitchChatClient client) : base(client) { }
        
        public Task JoinAsync()
            => throw new NotImplementedException();

        public Task LeaveAsync()
            => throw new NotImplementedException();
    }
}
