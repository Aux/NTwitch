using System;

namespace NTwitch.WebSocket
{
    public class SocketEntity<T> : IEntity<T>
    {
        public BaseSocketClient Client { get; }
        public T Id { get; }

        public SocketEntity(BaseSocketClient client, T id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<T>.Client
            => throw new NotImplementedException();
    }
}
