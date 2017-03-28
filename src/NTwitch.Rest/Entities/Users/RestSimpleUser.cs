using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.User;

namespace NTwitch.Rest
{
    public class RestSimpleUser : RestEntity<ulong>, IUser
    {
        /// <summary> The url for this user's logo </summary>
        public string LogoUrl { get; private set; }
        /// <summary> The name of this user </summary>
        public string Name { get; private set; }
        /// <summary> The display name of this user </summary>
        public string DisplayName { get; private set; }

        internal RestSimpleUser(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestSimpleUser Create(BaseRestClient client, Model model)
        {
            var entity = new RestSimpleUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            DisplayName = model.DisplayName;
            Name = model.Name;
        }

        // Channels
        /// <summary> Get information about this user's channel </summary>
        public Task<RestChannel> GetChannelAsync()
            => ClientHelper.GetChannelAsync(Client, Id);

        // Emotes
        /// <summary> Get all emotes available to this user </summary>
        public Task<IReadOnlyDictionary<string, IEnumerable<RestEmote>>> GetEmotesAsync()
            => UserHelper.GetEmotesAsync(Client, Id, Id);

        //// Subscriptions
        ///// <summary>  </summary>
        //public Task<IReadOnlyCollection<RestChannelSubscription>> GetSubscriptionsAsync()
        //    => UserHelper.GetSubscriptionsAsync(Client, Id);
        ///// <summary>  </summary>
        //public Task<RestChannelSubscription> GetSubscriptionAsync(ulong channelId)
        //    => UserHelper.GetSubscriptionAsync(Client, Id, channelId);

        // Follows
        /// <summary> Get all channels this user is following </summary>
        public Task<IReadOnlyCollection<RestChannelFollow>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, bool ascending = false, uint limit = 25, uint offset = 0)
            => UserHelper.GetFollowsAsync(Client, Id, sort, ascending, limit, offset);
        /// <summary> Get a specific channel follow by id </summary>
        public Task<RestChannelFollow> GetFollowAsync(ulong channelId)
            => UserHelper.GetFollowAsync(Client, Id, channelId);
        ///// <summary>  </summary>
        //public Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync()
        //    => UserHelper.GetFollowersAsync(Client, Id);
        ///// <summary>  </summary>
        //public Task<RestUserFollow> GetFollowerAsync(ulong userId)
        //    => UserHelper.GetFollowerAsync(Client, Id, userId);

        //// Blocks
        ///// <summary>  </summary>
        //public Task<IReadOnlyCollection<RestBlockedUser>> GetBlocksAsync(int limit, int offset)
        //    => UserHelper.GetBlocksAsync(Client, Id, limit, offset);
        ///// <summary>  </summary>
        //public Task BlockAsync()
        //    => UserHelper.BlockAsync(Client, Id);
        ///// <summary>  </summary>
        //public Task UnblockAsync()
        //    => UserHelper.UnblockAsync(Client, Id);

        //// VHS
        ///// <summary>  </summary>
        //public Task<string> CreateHeartbeatAsync()
        //    => UserHelper.CreateHeartbeatAsync(Client, Id);
        ///// <summary>  </summary>
        //public Task<string> GetHeartbeatAsync()
        //    => UserHelper.GetHeartbeatAsync(Client, Id);
        ///// <summary>  </summary>
        //public Task DeleteHeartbeatAsync()
        //    => UserHelper.DeleteHeartbeatAsync(Client, Id);
    }
}
