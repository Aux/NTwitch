using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestUser : RestEntity, IUser
    {
        public string Bio { get; }
        public DateTime CreatedAt { get; }
        public string DisplayName { get; }
        public string LogoUrl { get; }
        public string Name { get; }
        public string Type { get; }
        public DateTime UpdatedAt { get; }
        
        public RestUser(TwitchRestClient client, ulong id) : base(client, id) { }

        public Task<RestBlockedUser> BlockAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestChannelFollow> GetFollowAsync(ulong userid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<RestBlockedUser> UnblockAsync()
        {
            throw new NotImplementedException();
        }

        //IUser

        async Task<IEnumerable<IChannelFollow>> IUser.GetFollowsAsync(SortMode mode, SortDirection direction, TwitchPageOptions options)
            => await GetFollowsAsync(mode, direction, options);
        async Task<IChannelFollow> IUser.GetFollowAsync(ulong userid)
            => await GetFollowAsync(userid);
        async Task<IBlockedUser> IUser.BlockAsync()
            => await BlockAsync();
        async Task<IBlockedUser> IUser.UnblockAsync()
            => await UnblockAsync();
    }
}
