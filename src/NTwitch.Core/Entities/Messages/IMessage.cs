using System;

namespace NTwitch
{
    public interface IMessage : IEntity<string>
    {
        DateTime UtcTimestamp { get; }
    }
}
