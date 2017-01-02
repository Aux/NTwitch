using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestSubscription : IEntity, ISubscription
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public DateTime CreatedAt { get; internal set; }

        internal RestSubscription(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
