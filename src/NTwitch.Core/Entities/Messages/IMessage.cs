using System;

namespace NTwitch
{
    public interface IMessage : IEntity
    {
        DateTime UtcTimestamp { get; }
    }
}
