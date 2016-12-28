namespace NTwitch.WebSocket
{
    public class SocketChannelFollow : SocketFollow, IChannelFollow
    {
        public IChannel Channel { get; }

        internal SocketChannelFollow(TwitchSocketClient client, ulong id) : base(client, id) { }
    }
}
