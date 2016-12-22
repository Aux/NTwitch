namespace NTwitch.Chat
{
    public abstract class ChatEntity
    {
        public TwitchChatClient Client { get; }
        public ulong Id { get; }
        
        public ChatEntity(TwitchChatClient client, ulong id)
        {
            Client = client;
            Id = id;
        }
    }
}
