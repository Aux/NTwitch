using System;

namespace Twitch
{
    public interface IChannelSubscription
    {
        DateTime CreatedAt { get; }
        string Id { get; }
        IChannel Channel { get; }
        string[] Links { get; }
    }
}
