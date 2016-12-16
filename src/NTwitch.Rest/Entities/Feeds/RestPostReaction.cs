using System;
using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RestPostReaction : IPostReaction
    {
        public string Name { get; }
        public IEnumerable<uint> UserIds { get; }
    }
}
