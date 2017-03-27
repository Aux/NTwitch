using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.User;

namespace NTwitch.Rest
{
    public class RestSimpleUser : RestEntity<ulong>, IUser
    {
        public string LogoUrl { get; private set; }
        public string Name { get; private set; }
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
        public Task<RestSelfChannel> GetChannelAsync()
            => ClientHelper.GetCurrentChannelAsync(Client);

        // Follows
        public Task<IReadOnlyCollection<RestChannelFollow>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, bool ascending = false, uint limit = 25, uint offset = 0)
            => UserHelper.GetFollowsAsync(this, sort, ascending, limit, offset);
        public Task<RestChannelFollow> GetFollowAsync(ulong channelId)
            => UserHelper.GetFollowAsync(this, channelId);

        // Emotes
        public Task<IReadOnlyDictionary<string, IEnumerable<RestEmote>>> GetEmotesAsync()
            => UserHelper.GetEmotesAsync(this, Id);


        //// Follows
        //public Task<RestUserFollow> GetFollowersAsync()
        //    => UserHelper.GetFollowersAsync(this);
        //public Task<IReadOnlyCollection<RestUserFollow>> GetFollowerAsync(ulong userId)
        //    => UserHelper.GetFollowerAsync(this, userId);

        //// Subscriptions
        //public Task<IReadOnlyCollection<RestChannelSubscription>> GetSubscriptionsAsync()
        //    => UserHelper.GetSubscriptionsAsync(this);
        //public Task<RestChannelSubscription> GetSubscriptionAsync(ulong channelId)
        //    => UserHelper.GetSubscriptionAsync(this, channelId);

        //// Blocks
        //public Task<IReadOnlyCollection<RestBlockedUser>> GetBlocksAsync(int limit, int offset)
        //    => UserHelper.GetBlocksAsync(this, Id, limit, offset);

        // VHS
        //public Task<string> CreateHeartbeatAsync()
        //    => UserHelper.CreateHeartbeatAsync(this);
        //public Task<string> GetHeartbeatAsync()
        //    => UserHelper.GetHeartbeatAsync(this);
        //public Task DeleteHeartbeatAsync()
        //    => UserHelper.DeleteHeartbeatAsync(this);

        // Blocks
        //public Task BlockAsync()
        //    => UserHelper.BlockAsync(this, Id);
        //public Task UnblockAsync()
        //    => UserHelper.UnblockAsync(this, Id);
    }
}
