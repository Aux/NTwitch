using System;

namespace NTwitch.Helix
{
    public interface IEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary> Gets the ITwitchClient that created this object. </summary>
        ITwitchClient Twitch { get; }
        /// <summary> Gets the unique identifier for this object. </summary>
        TId Id { get; }
    }
}
