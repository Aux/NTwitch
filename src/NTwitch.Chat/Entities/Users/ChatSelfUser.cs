using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatSelfUser : ChatUser
    {
        public ChatSelfUser(TwitchChatClient client) : base(client) { }

        public static new ChatSelfUser Create(BaseRestClient client, string msg)
        {
            var user = new ChatSelfUser(client as TwitchChatClient);
            ChatHandler.PopulateObject(msg, user, client);
            return user;
        }
    }
}
