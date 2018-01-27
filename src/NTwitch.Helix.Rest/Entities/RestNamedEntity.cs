using System;

namespace NTwitch.Helix.Rest
{
    public abstract class RestNamedEntity<T> : RestEntity<T>
        where T : IEquatable<T>
    {
        public string Name { get; private set; }

        internal RestNamedEntity(BaseTwitchClient twitch, T id, string name)
            : base(twitch, id)
        {
            Name = name;
        }
    }
}
