namespace NTwitch.WebSocket
{
    public class SocketEntity : IEntity
    {
        public TwitchSocketClient Client { get; }
        public ulong Id { get; }
        
        public SocketEntity(TwitchSocketClient client, ulong id)
        {
            Client = client;
            Id = id;
        }
    }
}
