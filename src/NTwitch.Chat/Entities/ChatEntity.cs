using System;

namespace NTwitch.Chat
{
    public class ChatEntity<T> : IEntity<T>
    {
        /// <summary> An instance of the client that created this entity </summary>
        public TwitchChatClient Client { get; }
        /// <summary> The unique identifier for this entity </summary>
        public T Id { get; }

        public ChatEntity(TwitchChatClient client, T id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<T>.Client
            => throw new NotImplementedException();
    }
}
