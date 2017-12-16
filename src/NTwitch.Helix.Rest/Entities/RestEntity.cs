using System;

namespace NTwitch.Helix.Rest
{
    public abstract class RestEntity<T> : IEntity<T>
        where T : IEquatable<T>
    {
        internal BaseTwitchClient Twitch { get; }
        public T Id { get; }

        ITwitchClient IEntity<T>.Twitch => Twitch;

        internal RestEntity(BaseTwitchClient twitch, T id)
        {
            Twitch = twitch;
            Id = id;
        }
    }
}
