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

        internal static RestBlockedUser Create(BaseRestClient client, string json)
        {
            var user = new RestBlockedUser(client);
            JsonConvert.PopulateObject(json, user);
            return user;
        }

        IUser IBlockedUser.User
            => User;
    }
}
