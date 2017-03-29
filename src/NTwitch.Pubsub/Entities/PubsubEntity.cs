using System;

namespace NTwitch.Pubsub
{
    public class PubsubEntity<T> : IEntity<T>
    {
        public BasePubsubClient Client { get; }
        public T Id { get; }

        public PubsubEntity(BasePubsubClient client, T id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<T>.Client
            => throw new NotImplementedException();
    }
}
