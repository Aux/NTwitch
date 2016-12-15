using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public interface IGame
    {
        uint Id { get; }
        uint GiantBombId { get; }
        string Name { get; }
        TwitchImage BoxArt { get; }
        TwitchImage Logo { get; }
    }
}
