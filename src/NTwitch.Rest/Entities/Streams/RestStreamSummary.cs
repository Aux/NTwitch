using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public class RestStreamSummary : IStreamSummary
    {
        public int Channels { get; }
        public string[] Links { get; }
        public int Viewers { get; }
    }
}
