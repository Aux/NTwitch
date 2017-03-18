using System;

namespace NTwitch.Pubsub
{
    public class SocketEntity<T> : IEntity<T>
    {
        public BasePubsubClient Client { get; }
        public T Id { get; }

        public SocketEntity(BasePubsubClient client, T id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<T>.Client
            => throw new NotImplementedException();
    }
}
