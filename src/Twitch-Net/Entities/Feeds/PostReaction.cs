using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public class PostReaction
    {
        public string Name { get; }
        public IEnumerable<uint> UserIds { get; }
    }
}
