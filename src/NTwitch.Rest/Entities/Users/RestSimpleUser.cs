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
            => UserHelper.GetEmotesAsync(this, Id);

        //// Subscriptions
        ///// <summary>  </summary>
        //public Task<IReadOnlyCollection<RestChannelSubscription>> GetSubscriptionsAsync()
        //    => UserHelper.GetSubscriptionsAsync(this);
        ///// <summary>  </summary>
        //public Task<RestChannelSubscription> GetSubscriptionAsync(ulong channelId)
        //    => UserHelper.GetSubscriptionAsync(this, channelId);

        // Follows
        /// <summary> Get all channels this user is following </summary>
        public Task<IReadOnlyCollection<RestChannelFollow>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, bool ascending = false, uint limit = 25, uint offset = 0)
            => UserHelper.GetFollowsAsync(this, sort, ascending, limit, offset);
        /// <summary> Get a specific channel follow by id </summary>
        public Task<RestChannelFollow> GetFollowAsync(ulong channelId)
            => UserHelper.GetFollowAsync(this, channelId);
        ///// <summary>  </summary>
        //public Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync()
        //    => UserHelper.GetFollowersAsync(this);
        ///// <summary>  </summary>
        //public Task<RestUserFollow> GetFollowerAsync(ulong userId)
        //    => UserHelper.GetFollowerAsync(this, userId);

        //// Blocks
        ///// <summary>  </summary>
        //public Task<IReadOnlyCollection<RestBlockedUser>> GetBlocksAsync(int limit, int offset)
        //    => UserHelper.GetBlocksAsync(this, Id, limit, offset);
        ///// <summary>  </summary>
        //public Task BlockAsync()
        //    => UserHelper.BlockAsync(this, Id);
        ///// <summary>  </summary>
        //public Task UnblockAsync()
        //    => UserHelper.UnblockAsync(this, Id);

        //// VHS
        ///// <summary>  </summary>
        //public Task<string> CreateHeartbeatAsync()
        //    => UserHelper.CreateHeartbeatAsync(this);
        ///// <summary>  </summary>
        //public Task<string> GetHeartbeatAsync()
        //    => UserHelper.GetHeartbeatAsync(this);
        ///// <summary>  </summary>
        //public Task DeleteHeartbeatAsync()
        //    => UserHelper.DeleteHeartbeatAsync(this);
    }
}
