using System.Collections.Generic;

namespace NTwitch
{
    public class TopClipsParams
    {
        public HashSet<string> Channels { get; set; }
        public HashSet<string> Games { get; set; }
        public uint Limit { get; set; } = 10;
        public string Period { get; set; } = "week";
        public bool IsTrending { get; set; } = false;
    }
}
