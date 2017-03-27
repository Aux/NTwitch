namespace NTwitch.Pubsub
{
    public class PubsubUser : PubsubEntity<ulong>, IUser
    {
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        
        public PubsubUser(BasePubsubClient client, ulong id) 
            : base(client, id) { }
    }
}
