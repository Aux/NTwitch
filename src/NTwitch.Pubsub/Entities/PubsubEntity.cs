namespace NTwitch.WebSocket
{
    public class PubsubEntity : IEntity
    {
        public TwitchPubsubClient Client { get; }
        public ulong Id { get; }
        
        public PubsubEntity(TwitchPubsubClient client, ulong id)
        {
            Client = client;
            Id = id;
        }
    }
}
