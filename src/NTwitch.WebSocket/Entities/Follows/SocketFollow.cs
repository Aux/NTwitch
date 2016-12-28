using System;

namespace NTwitch.WebSocket
{
    public class SocketFollow : SocketEntity, IFollow
    {
        public DateTime CreatedAt { get; }
        public bool IsNotificationEnabled { get; }

        internal SocketFollow(TwitchSocketClient client, ulong id) : base(client, id) { }
    }
}
