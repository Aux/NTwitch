namespace NTwitch.WebSocket
{
    public class SocketEmoteImage : SocketEntity, IEmoteImage
    {
        public int Height { get; }
        public string ImageUrl { get; }
        public ulong SetId { get; }
        public int Width { get; }

        internal SocketEmoteImage(TwitchSocketClient client, ulong id) : base(client, id) { }
    }
}
