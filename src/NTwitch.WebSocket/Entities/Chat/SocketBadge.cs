namespace NTwitch.WebSocket
{
    public class SocketBadge : SocketEntity, IBadge
    {
        public string AlphaUrl { get; }
        public string ImageUrl { get; }
        public string Name { get; }
        public string SvgUrl { get; }

        public SocketBadge(TwitchSocketClient client, ulong id) : base(client, id) { }
    }
}
