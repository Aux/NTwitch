using System.Collections.Generic;

namespace NTwitch.WebSocket
{
    public class SocketEmote : SocketEntity, IEmote
    {
        public IEnumerable<IEmoteImage> Images { get; }
        public string Name { get; }

        internal SocketEmote(TwitchSocketClient client, ulong id) : base(client, id) { }
    }
}
