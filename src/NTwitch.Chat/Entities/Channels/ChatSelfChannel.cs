using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatSelfChannel : ChatChannel
    {
        public ChatSelfChannel(TwitchChatClient client) : base(client) { }
    }
}
