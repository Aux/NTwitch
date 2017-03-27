using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatUser : ChatEntity<ulong>, IUser
    {
        public ChatUser(BaseRestClient client, ulong id) : base(client, id) { }
    }
}
