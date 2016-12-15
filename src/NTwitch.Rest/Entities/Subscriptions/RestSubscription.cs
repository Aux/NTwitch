using System;

namespace Twitch.Rest
{
    public class RestSubscription : ISubscription
    {
        public DateTime CreatedAt { get; }
        public string Id { get; }
        public string[] Links { get; }
    }
}
