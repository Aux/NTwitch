using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestBlockedUser : RestEntity<ulong>, IBlockedUser
    {
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; internal set; }
        [JsonProperty("user")]
        public RestUser User { get; internal set; }

        internal RestBlockedUser(BaseRestClient client) : base(client) { }
        
        IUser IBlockedUser.User
            => User;
    }
}
