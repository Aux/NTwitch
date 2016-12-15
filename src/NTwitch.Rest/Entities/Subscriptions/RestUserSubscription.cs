using System;

namespace Twitch.Rest
{
    public class RestUserSubscription : IUserSubscription
    {
        public DateTime CreatedAt { get; }
        public string Id { get; }
        public string[] Links { get; }
        public IUser User { get; }
    }
}
