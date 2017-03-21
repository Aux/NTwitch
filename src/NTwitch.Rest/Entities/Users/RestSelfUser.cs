using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.SelfUser;

namespace NTwitch.Rest
{
    public class RestSelfUser : RestUser
    {
        public string Email { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsPartner { get; private set; }
        public bool IsTwitterConnected { get; private set; }
        public RestUserNotifications Notifications { get; private set; }
        
        public RestSelfUser(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestSelfUser Create(BaseRestClient client, Model model)
        {
            var entity = new RestSelfUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            base.Update(model);
            Email = model.Email;
            IsVerified = model.IsVerified;
            IsPartner = model.IsPartner;
            IsTwitterConnected = model.IsTwitterConnected;
            Notifications.Update(model.Notifications);
        }

        public override async Task UpdateAsync()
        {
            var entity = await Client.RestClient.GetCurrentUserAsync().ConfigureAwait(false);
            Update(entity);
        }

        //// Follows
        //public Task<RestUserFollow> GetFollowersAsync()
        //    => UserHelper.GetFollowersAsync(this);
        //public Task<IEnumerable<RestUserFollow>> GetFollowerAsync(ulong userId)
        //    => UserHelper.GetFollowerAsync(this, userId);

        //// Subscriptions
        //public Task<IEnumerable<RestChannelSubscription>> GetSubscriptionsAsync()
        //    => UserHelper.GetSubscriptionsAsync(this);
        //public Task<RestChannelSubscription> GetSubscriptionAsync(ulong channelId)
        //    => UserHelper.GetSubscriptionAsync(this, channelId);

        //// Blocks
        //public Task<IEnumerable<RestBlockedUser>> GetBlocksAsync(int limit, int offset)
        //    => UserHelper.GetBlocksAsync(this, Id, limit, offset);

        //// VHS
        //public Task<string> CreateHeartbeatAsync()
        //    => UserHelper.CreateHeartbeatAsync(this);
        //public Task<string> GetHeartbeatAsync()
        //    => UserHelper.GetHeartbeatAsync(this);
        //public Task DeleteHeartbeatAsync()
        //    => UserHelper.DeleteHeartbeatAsync(this);
    }
}
