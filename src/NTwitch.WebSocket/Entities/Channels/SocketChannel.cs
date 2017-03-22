namespace NTwitch.WebSocket
{
    public class SocketChannel : SocketEntity<ulong>, IChannel
    {
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        
        public SocketChannel(BaseSocketClient client, ulong id) 
            : base(client, id) { }


    }
}
