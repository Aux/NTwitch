namespace NTwitch.WebSocket
{
    public class SocketUser : SocketEntity<ulong>, IUser
    {
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        
        public SocketUser(BaseSocketClient client, ulong id) 
            : base(client, id) { }
    }
}
