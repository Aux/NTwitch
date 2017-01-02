using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("NTwitch.Pubsub")]
namespace NTwitch.Rest
{
    public class RestUser : IEntity, IUser
    {
        public TwitchRestClient Client { get; }
        public ulong Id { get; internal set; }
        public string Bio { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public string DisplayName { get; internal set; }
        public string LogoUrl { get; internal set; }
        public string Name { get; internal set; }
        public string Type { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }

        internal RestUser(ITwitchClient client)
        {
            Client = client as TwitchRestClient;
        }

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
        ITwitchClient IEntity.Client
            => Client;
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
