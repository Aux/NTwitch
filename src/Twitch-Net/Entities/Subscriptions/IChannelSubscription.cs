using System;

namespace Twitch
{
    public interface IChannelSubscription : ISubscription
    {
        IChannel Channel { get; }
    }
}
