using System;

namespace NTwitch.Rest
{
    public class RestNamedEntity<TId> : RestEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary> The unique name sometimes used to identify this entity </summary>
        public string Name { get; }

        public RestNamedEntity(BaseTwitchClient client, TId id, string name)
            : base(client, id)
        {
            Name = name;
        }
        
        public override string ToString()
            => Name;
    }
}
