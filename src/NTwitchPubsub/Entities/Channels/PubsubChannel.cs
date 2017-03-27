namespace NTwitch.Pubsub
{
    public class PubsubChannel : PubsubEntity<ulong>, IChannel
    {
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        
        public PubsubChannel(BasePubsubClient client, ulong id) 
            : base(client, id) { }


    }
}
