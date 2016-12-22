namespace NTwitch.Rest
{
    public class RestEntity : IEntity
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; }

        public RestEntity(TwitchRestClient client, ulong id)
        {
            Client = client;
            Id = id;
        }
    }
}
