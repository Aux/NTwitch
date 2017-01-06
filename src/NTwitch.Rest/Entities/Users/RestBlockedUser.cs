using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestBlockedUser : IEntity, IBlockedUser
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
        public RestUser User { get; internal set; }

        internal RestBlockedUser(TwitchRestClient client)
        {
            Client = client;
        }

        public static RestBlockedUser Create(BaseTwitchClient client, string json)
        {
            var user = new RestBlockedUser(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, user);
            return user;
        }

        ITwitchClient IEntity.Client
            => Client;
        IUser IBlockedUser.User
            => User;
    }
}
