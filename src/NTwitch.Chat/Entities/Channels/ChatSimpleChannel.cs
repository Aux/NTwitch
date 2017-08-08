using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MsgEventModel = NTwitch.Chat.API.MessageReceivedEvent;
using RoomStateModel = NTwitch.Chat.API.RoomStateEvent;
using ClearChatModel = NTwitch.Chat.API.ClearChatEvent;
using NoticeModel = NTwitch.Chat.API.UserNoticeEvent;

namespace NTwitch.Chat
{
    public class ChatSimpleChannel : ChatEntity<ulong>, ISimpleChannel
    {
        /// <summary> This channel's internal twitch username </summary>
        public string Name { get; private set; }

        public ChatSelfUser CurrentUser => ChatChannelHelper.GetMyUser(Client, this);
        public IReadOnlyCollection<ChatMessage> Messages => Client.Cache.Messages.Where(x => x.Channel.Id == Id).ToArray();

        internal ChatSimpleChannel(TwitchChatClient client, ulong id) 
            : base(client, id) { }

        public bool Equals(ISimpleChannel other)
            => Id == other.Id;

        internal static ChatSimpleChannel Create(TwitchChatClient client, MsgEventModel model)
        {
            var entity = new ChatSimpleChannel(client, model.ChannelId);
            entity.Update(model);
            return entity;
        }

        internal static ChatSimpleChannel Create(TwitchChatClient client, RoomStateModel model)
        {
            var entity = new ChatSimpleChannel(client, model.ChannelId);
            entity.Update(model);
            return entity;
        }

        internal static ChatSimpleChannel Create(TwitchChatClient client, ClearChatModel model)
        {
            var entity = new ChatSimpleChannel(client, model.ChannelId);
            entity.Update(model);
            return entity;
        }

        internal static ChatSimpleChannel Create(TwitchChatClient client, NoticeModel model)
        {
            var entity = new ChatSimpleChannel(client, model.ChannelId);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(MsgEventModel model)
        {
            Name = model.ChannelName;
        }

        internal virtual void Update(RoomStateModel model)
        {
            Name = model.ChannelName;
        }

        internal virtual void Update(ClearChatModel model)
        {
            Name = model.ChannelName;
        }

        internal virtual void Update(NoticeModel model)
        {
            Name = model.ChannelName;
        }

        // Chat
        public IReadOnlyCollection<string> GetNames()
            => Client.Cache.GetNames(Name);

        public Task SendMessageAsync(string content, RequestOptions options = null)
            => ChatChannelHelper.SendMessageAsync(Client, this, content, options);

        public Task ClearChatAsync(IUser user, RequestOptions options = null)
            => ClearChatAsync(user.Name, options);
        public Task ClearChatAsync(string userName, RequestOptions options = null)
            => ClearChatAsync(userName, null, null, options);
        public Task ClearChatAsync(IUser user, string reason, uint? duration = null, RequestOptions options = null)
            => ClearChatAsync(user.Name, reason, duration, options);
        public Task ClearChatAsync(string userName, string reason, uint? duration = null, RequestOptions options = null)
            => ChatChannelHelper.ClearChatAsync(Client, this, userName, reason, duration, options);

        // Channels
        ///// <summary> Change properties of this channel </summary>
        //public Task ModifyAsync(Action<ModifyChannelParams> changes, RequestOptions options = null)
        //    => ChannelHelper.ModifyAsync(this, changes, options);

        // Chat
        /// <summary> Get cheer badges for this channel </summary>
        public Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(RequestOptions options = null)
            => ClientHelper.GetCheersAsync(Client, Id, options);
        /// <summary> Get chat badges for this channel </summary>
        public Task<RestChatBadges> GetChatBadgesAsync(RequestOptions options = null)
            => ChannelHelper.GetChatBadgesAsync(Client, Id, options);

        // Streams
        /// <summary> Get this channel's stream information, if available </summary>
        public Task<RestStream> GetStreamAsync(StreamType type = StreamType.Live, RequestOptions options = null)
            => ClientHelper.GetStreamAsync(Client, Id, type, options);

        // Teams
        /// <summary> Get all teams this channel is a member of </summary>
        public Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(RequestOptions options = null)
            => ChannelHelper.GetTeamsAsync(Client, Id, options);

        // Users
        /// <summary> Get information about this channel's user </summary>
        public Task<RestUser> GetUserAsync(RequestOptions options = null)
            => ClientHelper.GetUserAsync(Client, Id, options);
        /// <summary> Get information about this channel's user, if authenticated </summary>
        public Task<RestSelfUser> GetSelfUserAsync(RequestOptions options = null)
            => ClientHelper.GetCurrentUserAsync(Client, options);

        // Users
        /// <summary> Get all users following this channel </summary>
        public Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(bool ascending = false, PageOptions paging = null, RequestOptions options = null)
            => ChannelHelper.GetFollowersAsync(Client, Id, ascending, paging, options);
        /// <summary> Get all users authorized as an editor on this channel </summary>
        public Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(RequestOptions options = null)
            => ChannelHelper.GetEditorsAsync(Client, Id, options);
        /// <summary> Get all users subscribed to this channel </summary>
        public Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(bool ascending = false, PageOptions paging = null, RequestOptions options = null)
            => ChannelHelper.GetSubscribersAsync(Client, Id, ascending, paging, options);
        /// <summary> Get a specific user subscriber by id </summary>
        public Task<RestUserSubscription> GetSubscriberAsync(ulong userId, RequestOptions options = null)
            => ChannelHelper.GetSubscriberAsync(Client, Id, userId, options);

        // Videos
        /// <summary>  </summary>
        public Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(PageOptions paging = null, RequestOptions options = null)    // Add parameters at some point
            => ChannelHelper.GetVideosAsync(Client, Id, paging, options);
        /// <summary>  </summary>
        public Task<IReadOnlyCollection<RestClip>> GetClipsAsync(bool istrending = false, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetFollowedClipsAsync(Client, Id, istrending, paging, options);

        // ISimpleChannel
        string ISimpleChannel.DisplayName
            => Name;
        Task ISimpleChannel.ModifyAsync(Action<ModifyChannelParams> changes, RequestOptions options)
            => Task.CompletedTask;
        Task<IReadOnlyCollection<ICheerInfo>> ISimpleChannel.GetCheersAsync(RequestOptions options)
            => Task.FromResult<IReadOnlyCollection<ICheerInfo>>(null);
        Task<IChatBadges> ISimpleChannel.GetChatBadgesAsync(RequestOptions options)
            => Task.FromResult<IChatBadges>(null);
        Task<IStream> ISimpleChannel.GetStreamAsync(StreamType type, RequestOptions options)
            => Task.FromResult<IStream>(null);
        Task<IReadOnlyCollection<ISimpleTeam>> ISimpleChannel.GetTeamsAsync(RequestOptions options)
            => Task.FromResult<IReadOnlyCollection<ISimpleTeam>>(null);
        Task<IUser> ISimpleChannel.GetUserAsync(RequestOptions options)
            => Task.FromResult<IUser>(null);
        Task<ISelfUser> ISimpleChannel.GetSelfUserAsync(RequestOptions options)
            => Task.FromResult<ISelfUser>(null);
        Task<IReadOnlyCollection<IUser>> ISimpleChannel.GetEditorsAsync(RequestOptions options)
            => Task.FromResult<IReadOnlyCollection<IUser>>(null);
        Task<IUserSubscription> ISimpleChannel.GetSubscriberAsync(ulong userId, RequestOptions options)
            => Task.FromResult<IUserSubscription>(null);
        Task<IReadOnlyCollection<IUserFollow>> ISimpleChannel.GetFollowersAsync(bool ascending, uint limit, uint offset, RequestOptions options)
            => Task.FromResult<IReadOnlyCollection<IUserFollow>>(null);
        Task<IReadOnlyCollection<IUserSubscription>> ISimpleChannel.GetSubscribersAsync(bool ascending, uint limit, uint offset, RequestOptions options)
            => Task.FromResult<IReadOnlyCollection<IUserSubscription>>(null);
        Task<IReadOnlyCollection<IVideo>> ISimpleChannel.GetVideosAsync(uint limit, uint offset, RequestOptions options)
            => Task.FromResult<IReadOnlyCollection<IVideo>>(null);
        Task<IReadOnlyCollection<IClip>> ISimpleChannel.GetClipsAsync(bool istrending, uint limit, RequestOptions options)
            => Task.FromResult<IReadOnlyCollection<IClip>>(null);
    }
}
