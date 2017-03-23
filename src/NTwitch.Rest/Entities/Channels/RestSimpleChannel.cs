using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Channel;

namespace NTwitch.Rest
{
    public class RestSimpleChannel : RestEntity<ulong>, IChannel
    {
        public string Name { get; private set; }
        public string DisplayName { get; private set; }

        internal RestSimpleChannel(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestSimpleChannel Create(BaseRestClient client, Model model)
        {
            var entity = new RestSimpleChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            DisplayName = model.DisplayName;
            Name = model.Name;
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        // Users
        public Task<IEnumerable<RestUserFollow>> GetFollowersAsync(bool ascending = false, uint limit = 25, uint offset = 0)
            => ChannelHelper.GetFollowersAsync(this, ascending, limit, offset);
        public Task<IEnumerable<RestUserSubscription>> GetSubscribersAsync(bool ascending = false, uint limit = 25, uint offset = 0)
            => ChannelHelper.GetSubscribersAsync(this, ascending, limit, offset);
        public Task<RestUser> GetSubscriberAsync(ulong userId)
            => ChannelHelper.GetSubscriberAsync(this, userId);

        // Teams
        public Task<IEnumerable<RestSimpleTeam>> GetTeamsAsync(uint limit = 25, uint offset = 0)
            => ChannelHelper.GetTeamsAsync(this, limit, offset);

        // Cheers
        public Task<IEnumerable<RestCheerInfo>> GetCheersAsync()
            => ClientHelper.GetCheersAsync(Client, Id);

        // Videos
        public Task<IEnumerable<RestVideo>> GetVideosAsync(uint limit = 25, uint offset = 0)    // Add parameters at some point
            => ChannelHelper.GetVideosAsync(this, limit, offset);
    }
}
