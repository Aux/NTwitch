using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class UserBase
    {
        internal BaseRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }

        internal UserBase(BaseRestClient client)
        {
            Client = client;
        }

        public Task<RestBlockedUser> BlockAsync()
            => throw new NotImplementedException();
        
        public Task<RestChannelFollow> GetFollowAsync(ulong userid)
            => throw new NotImplementedException();

        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null)
            => throw new NotImplementedException();

        public Task<RestBlockedUser> UnblockAsync()
            => throw new NotImplementedException();
    }
}
