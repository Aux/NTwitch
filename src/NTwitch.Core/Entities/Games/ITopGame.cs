using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITopGame : IEntity
    {
        int Channels { get; }
        int Viewers { get; }
        IGame Game { get; }
    }
}
