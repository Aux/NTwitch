using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class ChatEntity<T> : IEntity<T>
    {
        internal TwitchChatClient Client { get; }
        [ChatProperty("room-id")]
        public T Id { get; internal set; }

        internal ChatEntity(BaseRestClient client)
        {
            Client = client as TwitchChatClient;
        }

        ITwitchClient IEntity<T>.Client
            => Client;
    }
}
