using NTwitch.Rest.API;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public abstract class BaseTwitchClient : ITwitchClient
    {
        internal readonly AsyncEvent<Func<LogMessage, Task>> _logEvent = new AsyncEvent<Func<LogMessage, Task>>();
        public event Func<LogMessage, Task> Log { add { _logEvent.Add(value); } remove { _logEvent.Remove(value); } }

        internal readonly AsyncEvent<Func<Task>> _loggedInEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> LoggedIn { add { _loggedInEvent.Add(value); } remove { _loggedInEvent.Remove(value); } }
        internal readonly AsyncEvent<Func<Task>> _loggedOutEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> LoggedOut { add { _loggedOutEvent.Add(value); } remove { _loggedOutEvent.Remove(value); } }

        private readonly SemaphoreSlim _stateLock;
        private bool _disposed, _isFirstLogin;

        internal Logger RestLogger { get; }
        internal LogManager LogManager { get; }
        internal TwitchRestApiClient ApiClient { get; }
        public LoginState LoginState { get; private set; }
        public ISelfUser CurrentUser { get; protected set; }
        public ITokenInfo TokenInfo { get; protected set; }

        internal BaseTwitchClient(TwitchConfig config, TwitchRestApiClient client)
        {
            ApiClient = client;
            LogManager = new LogManager(config.LogLevel);
            LogManager.Message += async msg => await _logEvent.InvokeAsync(msg).ConfigureAwait(false);

            _stateLock = new SemaphoreSlim(1, 1);
            RestLogger = LogManager.CreateLogger("Rest");
            ApiClient.SentRequest += async (method, endpoint, ms) => await RestLogger.VerboseAsync($"{method} {endpoint}: {ms} ms");
        }

        internal virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                ApiClient.Dispose();
                _disposed = true;
            }
        }

        public void Dispose() => Dispose(true);

        public async Task LoginAsync(string token, bool validateToken = true)
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await LoginInternalAsync(token, validateToken).ConfigureAwait(false);
            }
            finally { _stateLock.Release(); }
        }

        private async Task LoginInternalAsync(string token, bool validateToken)
        {
            if (_isFirstLogin)
            {
                _isFirstLogin = false;
                await LogManager.WriteInitialLog().ConfigureAwait(false);
            }

            if (LoginState != LoginState.LoggedOut)
                await LogoutInternalAsync().ConfigureAwait(false);
            LoginState = LoginState.LoggingIn;

            try
            {
                await ApiClient.LoginAsync(token).ConfigureAwait(false);
                await OnLoginAsync(validateToken).ConfigureAwait(false);
                LoginState = LoginState.LoggedIn;
            }
            catch (Exception)
            {
                await LogoutInternalAsync().ConfigureAwait(false);
                throw;
            }

            await _loggedInEvent.InvokeAsync().ConfigureAwait(false);
        }

        internal virtual async Task OnLoginAsync(bool validateToken)
        {
            if (!validateToken) return;

            var tokenInfo = await GetTokenInfo().ConfigureAwait(false);
            if (!tokenInfo.IsValid)
            {
                await RestLogger.ErrorAsync("Token is not valid").ConfigureAwait(false);
                await LogoutAsync().ConfigureAwait(false);
                return;
            }

            TokenInfo = tokenInfo;
        }

        public async Task LogoutAsync()
        {
            await _stateLock.WaitAsync().ConfigureAwait(false);
            try
            {
                await LogoutInternalAsync().ConfigureAwait(false);
            }
            finally { _stateLock.Release(); }
        }

        private async Task LogoutInternalAsync()
        {
            if (LoginState == LoginState.LoggedOut)
                return;
            LoginState = LoginState.LoggingIn;

            await ApiClient.LogoutAsync().ConfigureAwait(false);

            await OnLogoutAsync().ConfigureAwait(false);
            CurrentUser = null;
            LoginState = LoginState.LoggedOut;

            await _loggedOutEvent.InvokeAsync().ConfigureAwait(false);
        }

        internal virtual Task OnLogoutAsync()
        {
            TokenInfo = null;
            return Task.Delay(0);
        }
        
        // Tokens
        /// <summary> Get information about the currently authorized token </summary>
        public async Task<ITokenInfo> GetTokenInfo(RequestOptions options = null)
        {
            if (TokenInfo != null)
                return TokenInfo;
            var token = await ClientHelper.GetTokenInfoAsync(this, options).ConfigureAwait(false);
            TokenInfo = token;
            return token;
        }

        // User
        /// <summary> Get the user associated with the authorized token </summary>
        public async Task<RestSelfUser> GetCurrentUserAsync(RequestOptions options = null)
        {
            var user = await ClientHelper.GetCurrentUserAsync(this, options).ConfigureAwait(false);
            CurrentUser = user;
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
        ConnectionState ITwitchClient.ConnectionState => ConnectionState.Disconnected;
        ISelfUser ITwitchClient.CurrentUser => CurrentUser;
        ITokenInfo ITwitchClient.TokenInfo => TokenInfo;

        Task ITwitchClient.ConnectAsync()
            => Task.Delay(0);
        Task ITwitchClient.DisconnectAsync()
            => Task.Delay(0);

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
