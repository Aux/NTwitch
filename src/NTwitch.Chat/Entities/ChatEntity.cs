using System;

namespace NTwitch.Chat
{
    public class ChatEntity<TId> : IEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary> An instance of the client that created this entity </summary>
        public TwitchChatClient Client { get; }
        /// <summary> The unique identifier for this entity </summary>
        public TId Id { get; }

        public ChatEntity(TwitchChatClient client, TId id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<TId>.Client
            => Client;
    }
}
