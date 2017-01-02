using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestFollow : IEntity, IFollow
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public bool IsNotificationEnabled { get; internal set; }

        internal RestFollow(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
    }
}
