using System;

namespace NTwitch.Pubsub
{
    public class PubsubEntity<T> : IEntity<T>
    {
        /// <summary> An instance of the client that created this entity </summary>
        public TwitchPubsubClient Client { get; }
        /// <summary> The unique identifier for this entity </summary>
        public T Id { get; }

        public PubsubEntity(TwitchPubsubClient client, T id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<T>.Client
            => throw new NotImplementedException();
    }
}
