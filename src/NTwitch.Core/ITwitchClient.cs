using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch
{
    public interface ITwitchClient : IDisposable
    {
        ISelfUser CurrentUser { get; }
        ConnectionState ConnectionState { get; }

        Task ConnectAsync();
        Task DisconnectAsync();

        // Tokens
        Task<IToken> GetTokenInfo(RequestOptions options = null);

        // Clips
        Task<IClip> GetClipAsync(string clipId);
        Task<IReadOnlyCollection<IClip>> GetTopClipsAsync(Action<TopClipsParams> options);

        // Communities
        Task<ICommunity> GetCommunityAsync(string communityId, bool isname = false);
        Task<IReadOnlyCollection<ITopCommunity>> GetTopCommunitiesAsync(uint limit = 10);

        // Channels
        Task<ISelfChannel> GetCurrentChannelAsync();
        Task<IChannel> GetChannelAsync(ulong channelId);
        Task<IReadOnlyCollection<IChannel>> FindChannelAsync(string query, uint limit = 25, uint offset = 0);

        // Ingests
        Task<IReadOnlyCollection<IIngest>> GetIngestsAsync();

        // Streams
        Task<IStream> GetStreamAsync(ulong channelId, StreamType type = StreamType.Live);
        Task<IReadOnlyCollection<IStream>> GetStreamsAsync(params ulong[] channelIds);
        Task<IReadOnlyCollection<IStream>> GetStreamsAsync(Action<GetStreamsParams> options);
        Task<IGameSummary> GetGameSummaryAsync(string game);
        Task<IReadOnlyCollection<IFeaturedStream>> GetFeaturedStreamsAsync(uint limit = 25, uint offset = 0);
        Task<IReadOnlyCollection<IGame>> FindGamesAsync(string query, bool islive = false);
        Task<IReadOnlyCollection<IStream>> FindStreamsAsync(string query, bool? hls = null, uint limit = 25, uint offset = 0);

        // Teams
        Task<IReadOnlyCollection<ISimpleTeam>> GetTeamsAsync(uint limit = 25, uint offset = 0);
        Task<ITeam> GetTeamAsync(string name);

        // Users
        Task<ISelfUser> GetCurrentUserAsync();
        Task<IUser> GetUserAsync(ulong userId);
        Task<IReadOnlyCollection<IUser>> GetUsersAsync(params string[] usernames);

        // Videos
        Task<IVideo> GetVideoAsync(string videoId);
    }
}
