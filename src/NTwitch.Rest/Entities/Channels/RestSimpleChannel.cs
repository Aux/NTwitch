using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Channel;

namespace NTwitch.Rest
{
    public class RestSimpleChannel : RestEntity<ulong>, IChannel
    {
        /// <summary> This channel's internal twitch username </summary>
        public string Name { get; private set; }
        /// <summary> This channel's display username </summary>
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
        /// <summary> Change properties of this channel </summary>
        public Task ModifyAsync(Action<ModifyChannelParams> options)
            => ChannelHelper.ModifyChannelAsync(this, options);

        // Users
        /// <summary> Get all users following this channel </summary>
        public Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(bool ascending = false, uint limit = 25, uint offset = 0)
            => ChannelHelper.GetFollowersAsync(Client, Id, ascending, limit, offset);
        /// <summary> Get all users authorized as an editor on this channel </summary>
        public Task<IReadOnlyCollection<RestUser>> GetEditorsAsync()
            => ChannelHelper.GetEditorsAsync(Client, Id);
        /// <summary> Get all users subscribed to this channel </summary>
        public Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(bool ascending = false, uint limit = 25, uint offset = 0)
            => ChannelHelper.GetSubscribersAsync(Client, Id, ascending, limit, offset);
        /// <summary> Get a specific user subscriber by id </summary>
        public Task<RestUserSubscription> GetSubscriberAsync(ulong userId)
            => ChannelHelper.GetSubscriberAsync(Client, Id, userId);

        // Chat
        /// <summary> Get cheer badges for this channel </summary>
        public Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync()
            => RestHelper.GetCheersAsync(Client, Id);
        /// <summary> Get chat badges for this channel </summary>
        public Task<RestChatBadges> GetChatBadgesAsync()
            => ChannelHelper.GetChatBadgesAsync(Client, Id);

        // Teams
        /// <summary> Get all teams this channel is a member of </summary>
        public Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync()
            => ChannelHelper.GetTeamsAsync(Client, Id);

        // Videos
        //public Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(uint limit = 25, uint offset = 0)    // Add parameters at some point
        //    => ChannelHelper.GetVideosAsync(Client, Id, limit, offset);
    }
}
