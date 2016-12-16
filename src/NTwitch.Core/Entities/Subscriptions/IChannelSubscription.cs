using System;

namespace NTwitch
{
    public interface IChannelSubscription : ISubscription
    {
        IChannel Channel { get; }
    }
}
