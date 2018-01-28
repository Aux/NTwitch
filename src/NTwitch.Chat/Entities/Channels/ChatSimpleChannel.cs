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
    public class ChatSimpleChannel : ChatNamedEntity<ulong>, IRestSimpleChannel
    {
        public ChatSelfUser CurrentUser => ChatChannelHelper.GetMyUser(Client, this);

        internal ChatSimpleChannel(TwitchChatClient client, ulong id, string name) 
            : base(client, id, name) { }

        public bool Equals(ISimpleChannel other)
            => Id == other.Id;

        internal static ChatSimpleChannel Create(TwitchChatClient client, MsgEventModel model)
        {
            return new ChatSimpleChannel(client, model.ChannelId, model.ChannelName);
        }

        internal static ChatSimpleChannel Create(TwitchChatClient client, RoomStateModel model)
        {
            return new ChatSimpleChannel(client, model.ChannelId, model.ChannelName);
        }

        internal static ChatSimpleChannel Create(TwitchChatClient client, ClearChatModel model)
        {
            return new ChatSimpleChannel(client, model.ChannelId, model.ChannelName);
        }

        internal static ChatSimpleChannel Create(TwitchChatClient client, NoticeModel model)
        {
            return new ChatSimpleChannel(client, model.ChannelId, model.ChannelName);
        }
        
        // Cache
        public IReadOnlyCollection<string> GetNames()
            => Client.Cache.GetNames(Name);
        public IReadOnlyCollection<ChatMessage> GetMessages()
            => Client.Cache.Messages.Where(x => x.Channel.Id == Id).ToArray();

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
        /// <summary> Change properties of this channel </summary>
        public Task ModifyAsync(Action<ModifyChannelParams> changes, RequestOptions options = null)
            => ChannelHelper.ModifyAsync(Client, this, changes, options);

        // Chat
        /// <summary> Get cheer badges for this channel </summary>
        public Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(RequestOptions options = null)
            => ClientHelper.GetCheersAsync(Client, Id, options);
        /// <summary> Get chat badges for this channel </summary>
        public Task<RestChatBadges> GetChatBadgesAsync(RequestOptions options = null)
            => ChannelHelper.GetChatBadgesAsync(Client, Id, options);

        // Streams
        /// <summary> Get this channel's stream information, if available </summary>
        public Task<RestBroadcast> GetStreamAsync(StreamType type = StreamType.Live, RequestOptions options = null)
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
    }
}
