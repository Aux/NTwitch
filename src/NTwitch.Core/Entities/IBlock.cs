using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface IBlock
    {
        string[] Links { get; }
        DateTime UpdatedAt { get; }
        IPostUser Author { get; }
    }
}
