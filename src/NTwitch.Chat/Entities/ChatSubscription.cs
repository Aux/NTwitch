using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatSubscription : SubscriptionBase
    {
        public ChatUser User { get; private set; }

        public ChatSubscription(TwitchChatClient client) : base(client) { }

        public static ChatSubscription Create(BaseRestClient client, string msg)
        {
            var sub = new ChatSubscription(client as TwitchChatClient);
            ChatParser.PopulateObject(msg, sub);
            return sub;
        }
    }
}
