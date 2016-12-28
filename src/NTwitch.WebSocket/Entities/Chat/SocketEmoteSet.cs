using System.Collections.Generic;

namespace NTwitch.WebSocket
{
    public class SocketEmoteSet : SocketEntity, IEmoteSet
    {
        public IEnumerable<IEmote> Emotes { get; }
        
        public SocketEmoteSet(TwitchSocketClient client, ulong id) : base(client, id) { }
    }
}
