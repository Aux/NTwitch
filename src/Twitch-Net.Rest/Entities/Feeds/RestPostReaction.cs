using System;
using System.Collections.Generic;

namespace Twitch.Rest
{
    public class RestPostReaction : IPostReaction
    {
        public string Name { get; }
        public IEnumerable<uint> UserIds { get; }
    }
}
