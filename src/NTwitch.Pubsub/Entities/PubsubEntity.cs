using System;

namespace NTwitch.Pubsub
{
    public class PubsubEntity<TId> : IEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary> An instance of the client that created this entity </summary>
        public TwitchPubsubClient Client { get; }
        /// <summary> The unique identifier for this entity </summary>
        public TId Id { get; }

        public PubsubEntity(TwitchPubsubClient client, TId id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<TId>.Client
            => Client;
    }
}
