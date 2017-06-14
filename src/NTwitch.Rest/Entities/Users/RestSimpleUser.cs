using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.User;

namespace NTwitch.Rest
{
    public class RestSimpleUser : RestEntity<ulong>, ISimpleUser
    {
        /// <summary> The url for this user's avatar </summary>
        public string AvatarUrl { get; private set; }
        /// <summary> The display name of this user </summary>
        public string DisplayName { get; private set; }
        /// <summary> The name of this user </summary>
        public string Name { get; private set; }

        internal RestSimpleUser(BaseTwitchClient client, ulong id) 
            : base(client, id) { }
        
        public bool Equals(ISimpleUser other)
            => Id == other.Id;

        internal static RestSimpleUser Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestSimpleUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            AvatarUrl = model.AvatarUrl;
            DisplayName = model.DisplayName;
            Name = model.Name;
        }

        // Channels
        /// <summary> Get this user's channel </summary>
        public Task<RestChannel> GetChannelAsync(RequestOptions options = null)
            => ClientHelper.GetChannelAsync(Client, Id, options);

        // Chat
        /// <summary> Get all chat emotes available to this user </summary>
        public Task<IReadOnlyDictionary<string, IReadOnlyCollection<RestEmote>>> GetEmotesAsync(RequestOptions options = null)
            => UserHelper.GetEmotesAsync(Client, Id, options);

        // Follows
        /// <summary> Get all channel follows for this user </summary>
        public Task<IReadOnlyCollection<RestChannelFollow>> GetFollowsAsync(RequestOptions options = null)
            => UserHelper.GetFollowsAsync(this, options);
        /// <summary> Get a specific channel follow for this user </summary>
        public Task<RestChannelFollow> GetFollowAsync(ulong channelId, RequestOptions options = null)
            => UserHelper.GetFollowAsync(this, channelId, options);

        // Heartbeat
        /// <summary> Creates a connection between this user and VHS, requires `viewing_activity_read` </summary>
        public Task<string> CreateHeartbeatAsync(RequestOptions options = null)
            => UserHelper.CreateHeartbeatAsync(this, options);
        /// <summary> Checks whether this user is connected to VHS, requires `user_read` </summary>
        public Task<string> GetHeartbeatAsync(RequestOptions options = null)
            => UserHelper.GetHeartbeatAsync(this, options);
        /// <summary> Deletes the connection between this user and VHS, requires `viewing_activity_read` </summary>
        public Task DeleteHeartbeatAsync(RequestOptions options = null)
            => UserHelper.DeleteHeartbeatAsync(this, options);

        // Streams
        /// <summary> Get this user's stream </summary>
        public Task<RestStream> GetStreamAsync(StreamType type = StreamType.Live, RequestOptions options = null)
            => ClientHelper.GetStreamAsync(Client, Id, type);
        /// <summary> Get streams this user is following, requires `user_read` </summary>
        public Task<IReadOnlyCollection<RestStream>> GetFollowedStreamsAsync(StreamType type = StreamType.Live, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetFollowedStreamsAsync(Client, type, paging, options);

        // Subscriptions
        /// <summary> Get a specific channel subscription for this user, requires `user_subscriptions` </summary>
        public Task<RestChannelSubscription> GetSubscriptionAsync(ulong channelId, RequestOptions options = null)
            => UserHelper.GetSubscrptionAsync(this, options);

        // Users
        /// <summary> Block this user, requires `user_blocks_edit` </summary>
        public Task BlockAsync(RequestOptions options = null)
            => UserHelper.BlockAsync(this, options);
        /// <summary> Unblock this user, requires `user_blocks_edit` </summary>
        public Task UnblockAsync(RequestOptions options = null)
            => UserHelper.UnblockAsync(this, options);
        /// <summary> Get all users currently blocked by this user, requires `user_blocks_read` </summary>
        public Task<IReadOnlyCollection<RestBlockedUser>> GetBlocksAsync(PageOptions paging = null, RequestOptions options = null)
            => UserHelper.GetBlocksAsync(this, paging, options);

        // Videos
        /// <summary> Get clips from all channels this user is following, requires `user_read` </summary>
        public Task<IReadOnlyCollection<RestClip>> GetFollowedClipsAsync(bool istrending = false, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetFollowedClipsAsync(Client, Id, istrending, paging, options);
    }
}
