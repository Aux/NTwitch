using NTwitch.Rest;
using System;

namespace NTwitch.Chat
{
    public class ChatEntity<T> : IEntity<T>
    {
        public BaseRestClient Client { get; }
        public T Id { get; }

        public ChatEntity(BaseRestClient client, T id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<T>.Client
            => throw new NotImplementedException();
    }
}
