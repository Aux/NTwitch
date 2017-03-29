using NTwitch.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitsModel = NTwitch.Pubsub.API.BitsMessage;
using SelfModel = NTwitch.Pubsub.API.WhisperUser;
using WhisperModel = NTwitch.Pubsub.API.WhisperMessage;

namespace NTwitch.Pubsub
{
    public class PubsubSimpleUser : PubsubEntity<ulong>, IUser
    {
        public string Name { get; private set; }

        internal PubsubSimpleUser(BasePubsubClient client, ulong id)
            : base(client, id) { }

        internal static PubsubSimpleUser Create(BasePubsubClient client, BitsModel model)
        {
            var entity = new PubsubSimpleUser(client, model.UserId);
            entity.Update(model);
            return entity;
        }

        internal static PubsubSimpleUser Create(BasePubsubClient client, WhisperModel model)
        {
            var entity = new PubsubSimpleUser(client, model.FromId);
            entity.Update(model);
            return entity;
        }

        internal static PubsubSimpleUser Create(BasePubsubClient client, SelfModel model)
        {
            var entity = new PubsubSimpleUser(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(BitsModel model)
        {
            Name = model.ChannelName;
        }

        internal virtual void Update(WhisperModel model)
        {
            Name = model.Tags.Login;
        }

        internal virtual void Update(SelfModel model)
        {
            Name = model.Username;
        }

        // Users
        /// <summary> Get information about this user </summary>
        public Task GetUserAsync()
            => ClientHelper.GetUserAsync(Client, Id);

        // Channels
        /// <summary> Get information about this user's channel </summary>
        public Task<RestChannel> GetChannelAsync()
            => ClientHelper.GetChannelAsync(Client, Id);
        
        // Emotes
        /// <summary> Get all emotes available to this user </summary>
        public Task<IReadOnlyDictionary<string, IEnumerable<RestEmote>>> GetEmotesAsync()
            => UserHelper.GetEmotesAsync(Client, Id, Id);
        
        // Follows
        /// <summary> Get all channels this user is following </summary>
        public Task<IReadOnlyCollection<RestChannelFollow>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, bool ascending = false, uint limit = 25, uint offset = 0)
            => UserHelper.GetFollowsAsync(Client, Id, sort, ascending, limit, offset);
        /// <summary> Get a specific channel follow by id </summary>
        public Task<RestChannelFollow> GetFollowAsync(ulong channelId)
            => UserHelper.GetFollowAsync(Client, Id, channelId);
    }
}
