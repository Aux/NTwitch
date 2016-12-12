using System;

namespace Twitch.Rest
{
    public class RestChannelFollow : IChannelFollow
    {
        public IChannel Channel { get; }
        public DateTime CreatedAt { get; }
        public string[] Links { get; }
        public bool Notifications { get; }
    }
}
