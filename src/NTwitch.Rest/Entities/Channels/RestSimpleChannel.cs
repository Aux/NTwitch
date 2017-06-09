using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Channel;

namespace NTwitch.Rest
{
    public class RestSimpleChannel : RestEntity<ulong>, ISimpleChannel
    {
        /// <summary> This channel's internal twitch username </summary>
        public string Name { get; private set; }
        /// <summary> This channel's display username </summary>
        public string DisplayName { get; private set; }
        
        internal RestSimpleChannel(BaseTwitchClient client, ulong id) 
            : base(client, id) { }

        internal static RestSimpleChannel Create(BaseTwitchClient client, Model model)
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

        // IEqualityComparer
        public bool Equals(ISimpleChannel x, ISimpleChannel y) => x.Id == y.Id;
        public int GetHashCode(ISimpleChannel obj) => obj.GetHashCode();

        //// Channels
        ///// <summary> Change properties of this channel </summary>
        //public Task ModifyAsync(Action<ModifyChannelParams> changes, RequestOptions options = null)
        //    => ChannelHelper.ModifyAsync(this, changes, options);

        //// Chat
        ///// <summary> Get cheer badges for this channel </summary>
        //public Task<IReadOnlyCollection<RestCheerInfo>> GetCheersAsync(RequestOptions options = null)
        //    => ClientHelper.GetCheersAsync(Client, Id, options);
        ///// <summary> Get chat badges for this channel </summary>
        //public Task<RestChatBadges> GetChatBadgesAsync(RequestOptions options = null)
        //    => ChannelHelper.GetChatBadgesAsync(Client, Id, options);

        //// Streams
        ///// <summary> Get this channel's stream information, if available </summary>
        //public Task<RestStream> GetStreamAsync(StreamType type = StreamType.Live, RequestOptions options = null)
        //    => ClientHelper.GetStreamAsync(Client, Id, type, options);

        //// Teams
        ///// <summary> Get all teams this channel is a member of </summary>
        //public Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(RequestOptions options = null)
        //    => ChannelHelper.GetTeamsAsync(Client, Id, options);

        //// Users
        ///// <summary> Get information about this channel's user </summary>
        //public Task<RestUser> GetUserAsync(RequestOptions options = null)
        //    => ClientHelper.GetUserAsync(Client, Id, options);
        ///// <summary> Get information about this channel's user, if authenticated </summary>
        //public Task<RestSelfUser> GetSelfUserAsync(RequestOptions options = null)
        //    => ClientHelper.GetCurrentUserAsync(Client, options);
        
        //// Users
        ///// <summary> Get all users following this channel </summary>
        //public Task<IReadOnlyCollection<RestUserFollow>> GetFollowersAsync(bool ascending = false, uint limit = 25, uint offset = 0, RequestOptions options = null)
        //    => ChannelHelper.GetFollowersAsync(Client, Id, ascending, limit, offset, options);
        ///// <summary> Get all users authorized as an editor on this channel </summary>
        //public Task<IReadOnlyCollection<RestUser>> GetEditorsAsync(RequestOptions options = null)
        //    => ChannelHelper.GetEditorsAsync(Client, Id, options);
        ///// <summary> Get all users subscribed to this channel </summary>
        //public Task<IReadOnlyCollection<RestUserSubscription>> GetSubscribersAsync(bool ascending = false, uint limit = 25, uint offset = 0, RequestOptions options = null)
        //    => ChannelHelper.GetSubscribersAsync(Client, Id, ascending, limit, offset, options);
        ///// <summary> Get a specific user subscriber by id </summary>
        //public Task<RestUserSubscription> GetSubscriberAsync(ulong userId, RequestOptions options = null)
        //    => ChannelHelper.GetSubscriberAsync(Client, Id, userId, options);

        //// Videos
        ///// <summary>  </summary>
        //public Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(PageOptions paging = null, RequestOptions options = null)    // Add parameters at some point
        //    => ChannelHelper.GetVideosAsync(Client, Id, paging, options);
        ///// <summary>  </summary>
        //public Task<IReadOnlyCollection<RestClip>> GetClipsAsync(bool istrending = false, PageOptions paging = null, RequestOptions options = null)
        //    => ClientHelper.GetFollowedClipsAsync(Client, Id, istrending, paging, options);
        
        // ISimpleChannel
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
