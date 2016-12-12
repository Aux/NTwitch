using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public interface IBlock
    {
        string[] Links { get; }
        DateTime UpdatedAt { get; }
        IPostUser Author { get; }
    }
}
