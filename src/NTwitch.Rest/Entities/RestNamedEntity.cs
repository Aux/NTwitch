using System;

namespace NTwitch.Rest
{
    public abstract class RestNamedEntity<TId> : RestEntity<TId>
        where TId : IEquatable<TId>
    {
        public string Name { get; }

        internal RestNamedEntity(BaseTwitchClient client, TId id, string name)
            : base(client, id)
        {
            Name = name;
        }
        
        public override string ToString()
            => Name;
    }
}
