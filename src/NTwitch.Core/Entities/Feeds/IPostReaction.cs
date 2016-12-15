using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public interface IPostReaction
    {
        string Name { get; }
        IEnumerable<uint> UserIds { get; }
    }
}
