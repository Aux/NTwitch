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


        // Channels
        public Task ModifyAsync(Action<ModifyChannelParams> options)
            => ChannelHelper.ModifyChannelAsync(this, options);
        
        // Users
        public Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(bool ascending = false, uint limit = 25, uint offset = 0)
            => ChannelHelper.GetFollowersAsync(this, ascending, limit, offset);
        public Task<IReadOnlyCollection<RestUser>> GetEditorsAsync()
            => ChannelHelper.GetEditorsAsync(this);
        public Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(bool ascending = false, uint limit = 25, uint offset = 0)
            => ChannelHelper.GetSubscribersAsync(this, ascending, limit, offset);
        public Task<RestUserSubscription> GetSubscriberAsync(ulong userId)
            => ChannelHelper.GetSubscriberAsync(this, userId);

        // Chat
        public Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync()
            => ClientHelper.GetCheersAsync(Client, Id);
        public Task<RestChatBadges> GetChatBadgesAsync()
            => ChannelHelper.GetChatBadgesAsync(this);

        // Teams
        public Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync()
            => ChannelHelper.GetTeamsAsync(this);

        // Videos
        //public Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(uint limit = 25, uint offset = 0)    // Add parameters at some point
        //    => ChannelHelper.GetVideosAsync(this, limit, offset);
    }
}
