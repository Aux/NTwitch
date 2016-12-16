using System;

namespace NTwitch.Rest
{
    public class RestFollow : IFollow
    {
        public DateTime CreatedAt { get; }
        public string[] Links { get; }
        public bool Notifications { get; }
    }
}
