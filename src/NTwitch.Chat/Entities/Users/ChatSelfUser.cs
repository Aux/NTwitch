using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatSelfUser : ChatUser
    {
        public ChatSelfUser(TwitchChatClient client) : base(client) { }
    }
}
