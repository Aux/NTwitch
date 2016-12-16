using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestBlock : IBlock
    {
        public IPostUser Author { get; }
        public string[] Links { get; }
        public DateTime UpdatedAt { get; }
    }
}
