using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ISimpleChannel : IEntity<ulong>, IEquatable<ISimpleChannel>
    {
        string Name { get; }
        string DisplayName { get; }
        
        // Channels
        Task ModifyAsync(Action<ModifyChannelParams> changes, RequestOptions options = null);

        // Chat
        Task<IReadOnlyCollection<ICheerInfo>> GetCheersAsync(RequestOptions options = null);
        Task<IChatBadges> GetChatBadgesAsync(RequestOptions options = null);

        // Streams
        Task<IStream> GetStreamAsync(StreamType type = StreamType.Live, RequestOptions options = null);

        // Teams
        Task<IReadOnlyCollection<ISimpleTeam>> GetTeamsAsync(RequestOptions options = null);

        // Users
        Task<IUser> GetUserAsync(RequestOptions options = null);
        Task<ISelfUser> GetSelfUserAsync(RequestOptions options = null);
        Task<IReadOnlyCollection<IUser>> GetEditorsAsync(RequestOptions options = null);
        Task<IUserSubscription> GetSubscriberAsync(ulong userId, RequestOptions options = null);
        Task<IReadOnlyCollection<IUserFollow>> GetFollowersAsync(bool ascending = false, uint limit = 25, uint offset = 0, RequestOptions options = null);
        Task<IReadOnlyCollection<IUserSubscription>> GetSubscribersAsync(bool ascending = false, uint limit = 25, uint offset = 0, RequestOptions options = null);

        // Videos
        Task<IReadOnlyCollection<IVideo>> GetVideosAsync(uint limit = 25, uint offset = 0, RequestOptions options = null);
        Task<IReadOnlyCollection<IClip>> GetClipsAsync(bool istrending = false, uint limit = 10, RequestOptions options = null);
    }
}
