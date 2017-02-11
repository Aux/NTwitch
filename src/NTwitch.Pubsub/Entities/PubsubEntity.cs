using System;

namespace NTwitch.Pubsub
{
    public class PubsubEntity<T> : IEntity<T>
    {
        internal TwitchPubsubClient Client { get; }

        public PubsubEntity(TwitchPubsubClient client)
        {
            Client = client;
        }

        ITwitchClient IEntity<T>.Client
            => Client;
        T IEntity<T>.Id 
            => throw new NotSupportedException();
    }
}
