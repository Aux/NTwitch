using NTwitch.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Pubsub.API.BitsMessage;

namespace NTwitch.Pubsub
{
    public class PubsubSimpleChannel : PubsubEntity<ulong>, IChannel
    {
        public string Name { get; private set; }

        internal PubsubSimpleChannel(BasePubsubClient client, ulong id)
            : base(client, id) { }

        internal static PubsubSimpleChannel Create(BasePubsubClient client, Model model)
        {
            var entity = new PubsubSimpleChannel(client, model.ChannelId);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Name = model.ChannelName;
        }

        // Channels
        /// <summary> Get information about this channel </summary>
        public Task GetChannelAsync()
            => ClientHelper.GetChannelAsync(Client, Id);

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
            => ClientHelper.GetCheersAsync(Client, Id);
        /// <summary> Get chat badges for this channel </summary>
        public Task<RestChatBadges> GetChatBadgesAsync()
            => ChannelHelper.GetChatBadgesAsync(Client, Id);

        // Teams
        /// <summary> Get all teams this channel is a member of </summary>
        public Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync()
            => ChannelHelper.GetTeamsAsync(Client, Id);
    }
}
