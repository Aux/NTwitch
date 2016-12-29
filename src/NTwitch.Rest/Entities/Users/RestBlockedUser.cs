using System;

namespace NTwitch.Rest
{
    public class RestBlockedUser : RestEntity, IBlockedUser
    {
        public DateTime UpdatedAt { get; }
        public RestUser User { get; }

        public RestBlockedUser(TwitchRestClient client, ulong id) : base(client, id) { }

        //IBlockedUser
        IUser IBlockedUser.User
            => User;
    }
}
