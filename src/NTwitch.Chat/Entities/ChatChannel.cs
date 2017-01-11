using NTwitch.Rest;
using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    public class ChatChannel : BaseChannel
    {
        public string Name { get; private set; }

        public ChatChannel(TwitchChatClient client) : base(client) { }

        public static ChatChannel Create(BaseRestClient client, string msg)
        {
            var channel = new ChatChannel(client as TwitchChatClient);
            ChatParser.PopulateObject(msg, channel);
            return channel;
        }

        public Task JoinAsync()
            => throw new NotImplementedException();

        public Task LeaveAsync()
            => throw new NotImplementedException();
    }
}
