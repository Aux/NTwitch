using System;

namespace NTwitch.Rest
{
    public class RestSubscription : RestEntity, ISubscription
    {
        public DateTime CreatedAt { get; }

        public RestSubscription(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
