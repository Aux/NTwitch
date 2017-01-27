using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestUserSummary : RestEntity, IUser
    {
        [JsonProperty("logo")]
        public string LogoUrl { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; private set; }
        
        internal RestUserSummary(BaseRestClient client) : base(client) { }

        internal static RestUserSummary Create(BaseRestClient client, string json)
        {
            var user = new RestUserSummary(client);
            JsonConvert.PopulateObject(json, user);
            return user;
        }

        // Users
        public Task<RestBlockedUser> BlockAsync()
            => UserHelper.BlockAsync(this, Client);
        public Task<bool> IsFollowingAsync(ulong channelId)
            => UserHelper.IsFollowingAsync(this, Client, channelId);
        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync()
            => GetFollowsAsync();
        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, SortDirection direction = SortDirection.Descending, PageOptions options = null)
            => UserHelper.GetFollowsAsync(this, Client, mode, direction, options);
        public Task<RestBlockedUser> UnblockAsync()
            => UserHelper.UnblockAsync(this, Client);

        // IUser
        Task<bool> IUser.IsFollowingAsync(ulong channelId)
            => IsFollowingAsync(channelId);
        Task<IBlockedUser> IUser.BlockAsync()
            => null;
        Task<IBlockedUser> IUser.UnblockAsync()
            => null;
        Task<IEnumerable<IChannelFollow>> IUser.GetFollowsAsync()
            => null;
    }
}
