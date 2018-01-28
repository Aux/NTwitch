using NTwitch.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;
using MsgEventModel = NTwitch.Chat.API.MessageReceivedEvent;
using UserStateModel = NTwitch.Chat.API.UserStateEvent;
using ClearChatModel = NTwitch.Chat.API.ClearChatEvent;
using NoticeModel = NTwitch.Chat.API.UserNoticeEvent;
using System;

namespace NTwitch.Chat
{
    public class ChatSimpleUser : ChatNamedEntity<ulong>, IRestSimpleUser
    {
        /// <summary> The display name of this user </summary>
        public string DisplayName { get; private set; }

        internal ChatSimpleUser(TwitchChatClient client, ulong id, string name)
            : base(client, id, name) { }

        public bool Equals(ISimpleUser other)
            => Id == other.Id;
        public override string ToString()
            => DisplayName;

        internal static ChatSimpleUser Create(TwitchChatClient client, MsgEventModel model)
        {
            var entity = new ChatSimpleUser(client, model.UserId, model.UserName);
            entity.Update(model);
            return entity;
        }

        internal static ChatSimpleUser Create(TwitchChatClient client, UserStateModel model)
        {
            var entity = new ChatSimpleUser(client, client.TokenInfo.UserId, model.DisplayName);
            entity.Update(model);
            return entity;
        }

        internal static ChatSimpleUser Create(TwitchChatClient client, ClearChatModel model)
        {
            var entity = new ChatSimpleUser(client, client.TokenInfo.UserId, model.UserName);
            entity.Update(model);
            return entity;
        }

        internal static ChatSimpleUser Create(TwitchChatClient client, NoticeModel model)
        {
            var entity = new ChatSimpleUser(client, client.TokenInfo.UserId, model.Name);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(MsgEventModel model)
        {
            DisplayName = model.DisplayName;
        }

        internal virtual void Update(UserStateModel model)
        {
            DisplayName = model.DisplayName;
        }

        internal virtual void Update(ClearChatModel model)
        {
            DisplayName = model.UserName;
        }

        internal virtual void Update(NoticeModel model)
        {
            DisplayName = model.DisplayName;
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
        public Task<IReadOnlyCollection<RestChannelFollow>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, bool ascending = false, PageOptions paging = null, RequestOptions options = null)
            => UserHelper.GetFollowsAsync(Client, Id, sort, ascending, paging, options);
        /// <summary> Get a specific channel follow for this user </summary>
        public Task<RestChannelFollow> GetFollowAsync(ulong channelId, RequestOptions options = null)
            => UserHelper.GetFollowAsync(Client, Id, channelId, options);
        
        // Streams
        /// <summary> Get this user's stream </summary>
        public Task<RestBroadcast> GetStreamAsync(StreamType type = StreamType.Live, RequestOptions options = null)
            => ClientHelper.GetStreamAsync(Client, Id, type);
        /// <summary> Get streams this user is following, requires `user_read` </summary>
        public Task<IReadOnlyCollection<RestBroadcast>> GetFollowedStreamsAsync(StreamType type = StreamType.Live, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetFollowedStreamsAsync(Client, type, paging, options);

        // Subscriptions
        /// <summary> Get a specific channel subscription for this user, requires `user_subscriptions` </summary>
        public Task<RestChannelSubscription> GetSubscriptionAsync(ulong channelId, RequestOptions options = null)
            => UserHelper.GetSubscrptionAsync(Client, Id, channelId, options);

        // Users
        /// <summary> Get all users currently blocked by this user, requires `user_blocks_read` </summary>
        public Task<IReadOnlyCollection<RestBlockedUser>> GetBlocksAsync(PageOptions paging = null, RequestOptions options = null)
            => UserHelper.GetBlocksAsync(Client, Id, paging, options);

        // Videos
        /// <summary> Get clips from all channels this user is following, requires `user_read` </summary>
        public Task<IReadOnlyCollection<RestClip>> GetFollowedClipsAsync(bool istrending = false, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetFollowedClipsAsync(Client, Id, istrending, paging, options);

        // IUser
        string ISimpleUser.AvatarUrl => null;

        // Unimplemented
        Task IRestSimpleUser.BlockAsync(RequestOptions options)
            => throw new NotImplementedException();
        Task IRestSimpleUser.UnblockAsync(RequestOptions options)
            => throw new NotImplementedException();
    }
}
