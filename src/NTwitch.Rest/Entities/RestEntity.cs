using System;

namespace NTwitch.Rest
{
    public class RestEntity<TId> : IEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary> An instance of the client that created this entity </summary>
        public BaseTwitchClient Client { get; }
        /// <summary> The unique identifier for this entity </summary>
        public TId Id { get; }

        public RestEntity(BaseTwitchClient client, TId id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<TId>.Client
            => Client;
    }
}
