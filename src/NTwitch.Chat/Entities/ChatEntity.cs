namespace NTwitch.Chat
{
    public class ChatEntity
    {
        public TwitchChatClient Client { get; private set; }

        public ChatEntity(TwitchChatClient client)
        {
            Client = client;
        }
    }
}
