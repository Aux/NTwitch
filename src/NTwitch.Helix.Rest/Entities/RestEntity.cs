using System;

namespace NTwitch.Helix.Rest
{
    public abstract class RestEntity<T> : IEntity<T>
        where T : IEquatable<T>
    {
        /// <summary> An instance of the client which created this entity </summary>
        internal BaseTwitchClient Twitch { get; }
        /// <summary> The unique id number of this entity </summary>
        public T Id { get; }

        ITwitchClient IEntity<T>.Twitch => Twitch;

        internal RestEntity(BaseTwitchClient twitch, T id)
        {
            Twitch = twitch;
            Id = id;
        }
    }
}
