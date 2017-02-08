using System;

namespace NTwitch.Pubsub
{
    public class PubsubEntity : IEntity
    {
        internal TwitchPubsubClient Client { get; }

        public PubsubEntity(TwitchPubsubClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity.Client
            => Client;
        uint IEntity.Id 
            => throw new NotSupportedException();
    }
}
