using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatEntity : IEntity
    {
        internal TwitchChatClient Client { get; }
        [ChatProperty("room-id")]
        public ulong Id { get; internal set; }

        internal ChatEntity(BaseRestClient client)
        {
            Client = client as TwitchChatClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
