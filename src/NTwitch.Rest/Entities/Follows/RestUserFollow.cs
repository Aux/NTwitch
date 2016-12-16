using System;

namespace NTwitch.Rest
{
    public class RestUserFollow : IUserFollow
    {
        public DateTime CreatedAt { get; }
        public string[] Links { get; }
        public bool Notifications { get; }
        public IUser User { get; }
    }
}
