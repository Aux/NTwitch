using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public class RestChannelSubscription : IChannelSubscription
    {
        public IChannel Channel { get; }
        public DateTime CreatedAt { get; }
        public string Id { get; }
        public string[] Links { get; }
    }
}
