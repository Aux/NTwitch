using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestBlockedUser : IEntity, IBlockedUser
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
        public RestUser User { get; internal set; }

        internal RestBlockedUser(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

        ITwitchClient IEntity.Client
            => Client;
        IUser IBlockedUser.User
            => User;
    }
}
