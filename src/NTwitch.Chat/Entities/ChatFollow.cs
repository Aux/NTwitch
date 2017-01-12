using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatFollow : FollowBase
    {
        public ChatUser User { get; private set; }

        public ChatFollow(TwitchChatClient client) : base(client) { }

        public static ChatFollow Create(BaseRestClient client, string msg)
        {
            var follow = new ChatFollow(client as TwitchChatClient);
            ChatParser.PopulateObject(msg, follow);
            return follow;
        }
    }
}
