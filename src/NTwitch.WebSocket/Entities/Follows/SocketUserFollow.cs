namespace NTwitch.WebSocket
{
    public class SocketUserFollow : SocketFollow, IUserFollow
    {
        public IUser User { get; }

        internal SocketUserFollow(TwitchSocketClient client, ulong id) : base(client, id) { }
    }
}
