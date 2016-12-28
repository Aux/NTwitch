using System;

namespace NTwitch.Rest
{
    public class RestFollow : RestEntity, IFollow
    {
        public DateTime CreatedAt { get; }
        public bool IsNotificationEnabled { get; }

        internal RestFollow(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
