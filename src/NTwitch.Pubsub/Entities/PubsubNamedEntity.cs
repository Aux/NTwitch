using System;

namespace NTwitch.Pubsub
{
    public class PubsubNamedEntity<TId> : PubsubEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary> The unique name sometimes used to identify this entity </summary>
        public string Name { get; }

        public PubsubNamedEntity(TwitchPubsubClient client, TId id, string name)
            : base(client, id)
        {
            Name = name;
        }

        public override string ToString()
            => Name;
    }
}
