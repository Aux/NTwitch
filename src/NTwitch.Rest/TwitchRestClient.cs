using NTwitch.Rest.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class TwitchRestClient : BaseTwitchClient, ITwitchClient
    {
        private RestToken _restToken;

        public new RestSelfUser CurrentUser => base.CurrentUser as RestSelfUser;
        public new RestToken TokenInfo => _restToken;

        public TwitchRestClient() : this(new TwitchRestConfig()) { }
        public TwitchRestClient(TwitchRestConfig config) 
            :base(config, CreateApiClient(config)) { }

        private static TwitchRestApiClient CreateApiClient(TwitchRestConfig config)
            => new TwitchRestApiClient(config.RestClientProvider, config.ClientId, TwitchConfig.UserAgent);

        internal override void Dispose(bool disposing)
        {
            if (disposing)
                ApiClient.Dispose();
        }

        internal override async Task OnLoginAsync(bool validateToken)
        {
            if (!validateToken) return;

            var tokenInfo = await GetTokenInfo().ConfigureAwait(false); 
            if (!tokenInfo.IsValid)
            {
                await RestLogger.ErrorAsync("Token is not valid").ConfigureAwait(false);
                await LogoutAsync().ConfigureAwait(false);
                return;
            }

            ApiClient.CurrentUserId = tokenInfo.UserId;
            _restToken = tokenInfo;
        }

        internal override Task OnLogoutAsync()
        {
            _restToken = null;
            return Task.Delay(0);
        }
        
        // Tokens
        /// <summary> Get information about the currently authorized token </summary>
        public async Task<RestToken> GetTokenInfo(RequestOptions options = null)
        {
            if (TokenInfo != null) return TokenInfo;
            var token = await ClientHelper.GetTokenInfoAsync(this, options).ConfigureAwait(false);
            base.TokenInfo = token;
            return token;
        }
        
        // User
        /// <summary> Get the user associated with the authorized token </summary>
        public async Task<RestSelfUser> GetCurrentUserAsync(RequestOptions options = null)
        {
            var user = await ClientHelper.GetCurrentUserAsync(this, options).ConfigureAwait(false);
            base.CurrentUser = user;
            return user;
        }

        /// <summary> Get information about a user by id </summary>
        public Task<RestUser> GetUserAsync(ulong userId)
            => ClientHelper.GetUserAsync(this, userId);
        /// <summary> Get information about users by name </summary>
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(params string[] usernames)
            => ClientHelper.GetUsersAsync(this, usernames);

        // Clips
        /// <summary> Get information about a clip by id </summary>
        public Task<RestClip> GetClipAsync(string clipId)
            => ClientHelper.GetClipAsync(this, clipId);
        /// <summary> Get the most popular clips for the specified parameters </summary>
        public Task<IReadOnlyCollection<RestClip>> GetTopClipsAsync(Action<TopClipsParams> options)
            => ClientHelper.GetTopClipsAsync(this, options);

        // Community
        /// <summary> Get information about a community by id </summary>
        public Task<RestCommunity> GetCommunityAsync(string communityId, bool isname = false)
            => ClientHelper.GetCommunityAsync(this, communityId, isname);
        /// <summary> Get the most popular communities on twitch </summary>
        public Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(PageOptions paging = null)
            => ClientHelper.GetTopCommunitiesAsync(this, paging);

        // Channels
        /// <summary> Get the channel associated with the authorized token </summary>
        public Task<RestSelfChannel> GetCurrentChannelAsync()
            => ClientHelper.GetCurrentChannelAsync(this);
        /// <summary> Get information about a channel by id </summary>
        public Task<RestChannel> GetChannelAsync(ulong channelId)
            => ClientHelper.GetChannelAsync(this, channelId);
        /// <summary> Find channels relating to the specified query </summary>
        public Task<IReadOnlyCollection<RestChannel>> FindChannelAsync(string query, PageOptions paging = null)
            => ClientHelper.FindChannelAsync(this, query, paging);

        // Ingests
        /// <summary> Get information about twitch's ingest servers </summary>
        public Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync()
            => ClientHelper.GetIngestsAsync(this);
        
        // Streams
        /// <summary> Get information about a channel's stream </summary>
        public Task<RestStream> GetStreamAsync(ulong channelId, StreamType type = StreamType.Live)
            => ClientHelper.GetStreamAsync(this, channelId, type);
        /// <summary> Get the streams for the specified channels, if available </summary>
        public Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(params ulong[] channelIds)
            => ClientHelper.GetStreamsAsync(this, channelIds);
        /// <summary> Get the top viewed streams on twitch for the specified options </summary>
        public Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(Action<GetStreamsParams> options)
            => ClientHelper.GetStreamsAsync(this, options);
        /// <summary> Get a summary of popularity for the specified game </summary>
        public Task<RestGameSummary> GetGameSummaryAsync(string game)
            => ClientHelper.GetGameSummaryAsync(this, game);
        /// <summary> Get the streams that appear on the front page of twitch </summary>
        public Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(PageOptions paging = null)
            => ClientHelper.GetFeaturedStreamsAsync(this, paging);
        /// <summary> Find games relating to the specified query </summary>
        public Task<IReadOnlyCollection<RestGame>> FindGamesAsync(string query, bool islive = false)
            => ClientHelper.FindGamesAsync(this, query, islive);
        /// <summary> Find streams relating to the specified query </summary>
        public Task<IReadOnlyCollection<RestStream>> FindStreamsAsync(string query, bool? hls = null, PageOptions paging = null)
            => ClientHelper.FindStreamsAsync(this, query, hls, paging);

        // Teams
        /// <summary> Get all teams on twitch </summary>
        public Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(PageOptions paging = null)
            => ClientHelper.GetTeamsAsync(this, paging);
        /// <summary> Get a team by name </summary>
        public Task<RestTeam> GetTeamAsync(string name)
            => ClientHelper.GetTeamAsync(this, name);

        // Videos
        /// <summary> Get information about a video by id </summary>
        public Task<RestVideo> GetVideoAsync(string videoId)
            => ClientHelper.GetVideoAsync(this, videoId);
        /// <summary> Get the top videos on twitch based on viewcount </summary>
        public Task<IReadOnlyCollection<RestVideo>> GetTopVideosAsync(string game = null, string period = null, string broadcastType = null, string language = null, string sort = null, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetTopVideosAsync(this, game, period, broadcastType, language, sort, paging, options);
        /// <summary> Get videos from channels followed by the current user </summary>
        public Task<IReadOnlyCollection<RestVideo>> GetFollowedVideosAsync(string broadcastType = null, string language = null, string sort = null, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetFollowedVideosAsync(this, broadcastType, language, sort, paging, options);
        
        // ITwitchClient
        Task<ITokenInfo> ITwitchClient.GetTokenInfo(RequestOptions options)
            => Task.FromResult<ITokenInfo>(null);
        Task<IClip> ITwitchClient.GetClipAsync(string clipId)
            => Task.FromResult<IClip>(null);
        Task<IReadOnlyCollection<IClip>> ITwitchClient.GetTopClipsAsync(Action<TopClipsParams> options)
            => Task.FromResult<IReadOnlyCollection<IClip>>(null);
        Task<ICommunity> ITwitchClient.GetCommunityAsync(string communityId, bool isname)
            => Task.FromResult<ICommunity>(null);
        Task<IReadOnlyCollection<ITopCommunity>> ITwitchClient.GetTopCommunitiesAsync(uint limit)
            => Task.FromResult<IReadOnlyCollection<ITopCommunity>>(null);
        Task<ISelfChannel> ITwitchClient.GetCurrentChannelAsync()
            => Task.FromResult<ISelfChannel>(null);
        Task<IChannel> ITwitchClient.GetChannelAsync(ulong channelId)
            => Task.FromResult<IChannel>(null);
        Task<IReadOnlyCollection<IChannel>> ITwitchClient.FindChannelAsync(string query, uint limit, uint offset)
            => Task.FromResult<IReadOnlyCollection<IChannel>>(null);
        Task<IReadOnlyCollection<IIngest>> ITwitchClient.GetIngestsAsync()
            => Task.FromResult<IReadOnlyCollection<IIngest>>(null);
        Task<IStream> ITwitchClient.GetStreamAsync(ulong channelId, StreamType type)
            => Task.FromResult<IStream>(null);
        Task<IReadOnlyCollection<IStream>> ITwitchClient.GetStreamsAsync(params ulong[] channelIds)
            => Task.FromResult<IReadOnlyCollection<IStream>>(null);
        Task<IReadOnlyCollection<IStream>> ITwitchClient.GetStreamsAsync(Action<GetStreamsParams> options)
            => Task.FromResult<IReadOnlyCollection<IStream>>(null);
        Task<IGameSummary> ITwitchClient.GetGameSummaryAsync(string game)
            => Task.FromResult<IGameSummary>(null);
        Task<IReadOnlyCollection<IFeaturedStream>> ITwitchClient.GetFeaturedStreamsAsync(uint limit, uint offset)
            => Task.FromResult<IReadOnlyCollection<IFeaturedStream>>(null);
        Task<IReadOnlyCollection<IGame>> ITwitchClient.FindGamesAsync(string query, bool islive)
            => Task.FromResult<IReadOnlyCollection<IGame>>(null);
        Task<IReadOnlyCollection<IStream>> ITwitchClient.FindStreamsAsync(string query, bool? hls, uint limit, uint offset)
            => Task.FromResult<IReadOnlyCollection<IStream>>(null);
        Task<IReadOnlyCollection<ISimpleTeam>> ITwitchClient.GetTeamsAsync(uint limit, uint offset)
            => Task.FromResult<IReadOnlyCollection<ISimpleTeam>>(null);
        Task<ITeam> ITwitchClient.GetTeamAsync(string name)
            => Task.FromResult<ITeam>(null);
        Task<ISelfUser> ITwitchClient.GetCurrentUserAsync()
            => Task.FromResult<ISelfUser>(null);
        Task<IUser> ITwitchClient.GetUserAsync(ulong userId)
            => Task.FromResult<IUser>(null);
        Task<IReadOnlyCollection<IUser>> ITwitchClient.GetUsersAsync(params string[] usernames)
            => Task.FromResult<IReadOnlyCollection<IUser>>(null);
        Task<IVideo> ITwitchClient.GetVideoAsync(string videoId)
            => Task.FromResult<IVideo>(null);
    }
}
