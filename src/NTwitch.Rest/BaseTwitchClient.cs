using NTwitch.Rest.API;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public RestTokenInfo TokenInfo { get; protected set; }
        public ISelfUser CurrentUser { get; protected set; }
        
        internal BaseTwitchClient(TwitchConfig config, TwitchRestApiClient client)
        {
            ApiClient = client;
            LogManager = new LogManager(config.LogLevel);
            LogManager.Message += async msg => await _logEvent.InvokeAsync(msg).ConfigureAwait(false);

            _stateLock = new SemaphoreSlim(1, 1);
            RestLogger = LogManager.CreateLogger("Rest");
            ApiClient.SentRequest += async (method, endpoint, ms) => await RestLogger.VerboseAsync($"{method} /{endpoint}: {ms} ms");
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

            var tokenInfo = await GetTokenInfoAsync().ConfigureAwait(false);
            if (!tokenInfo.IsValid)
            {
                await RestLogger.ErrorAsync("Token is not valid").ConfigureAwait(false);
                await LogoutAsync().ConfigureAwait(false);
                return;
            }

            TokenInfo = tokenInfo;
            if (TokenInfo.Authorization.Scopes.Contains("user_read"))
                CurrentUser = await GetCurrentUserAsync().ConfigureAwait(false);
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
            TokenInfo = null;
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
        public async Task<RestTokenInfo> GetTokenInfoAsync(RequestOptions options = null)
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
            var user = await ClientHelper.GetCurrentUserAsync(this, options);
            CurrentUser = user;
            return user;
        }
        
        /// <summary> Get information about a user by id </summary>
        public Task<RestUser> GetUserAsync(ulong userId, RequestOptions options = null)
            => ClientHelper.GetUserAsync(this, userId, options);
        /// <summary> Get information about users by name </summary>
        public Task<IReadOnlyCollection<RestUser>> GetUsersAsync(params string[] usernames)
            => ClientHelper.GetUsersAsync(this, usernames);

        // Clips
        /// <summary> Get information about a clip by id </summary>
        public Task<RestClip> GetClipAsync(string clipId, RequestOptions options = null)
            => ClientHelper.GetClipAsync(this, clipId, options);
        /// <summary> Get the most popular clips for the specified parameters </summary>
        public Task<IReadOnlyCollection<RestClip>> GetTopClipsAsync(Action<TopClipsParams> parameters, RequestOptions options = null)
            => ClientHelper.GetTopClipsAsync(this, parameters, options);

        // Community
        /// <summary> Get information about a community by id </summary>
        public Task<RestCommunity> GetCommunityAsync(string communityId, bool isname = false, RequestOptions options = null)
            => ClientHelper.GetCommunityAsync(this, communityId, isname, options);
        /// <summary> Get the most popular communities on twitch </summary>
        public Task<IReadOnlyCollection<RestTopCommunity>> GetTopCommunitiesAsync(PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetTopCommunitiesAsync(this, paging, options);

        // Channels
        /// <summary> Get the channel associated with the authorized token </summary>
        public Task<RestSelfChannel> GetCurrentChannelAsync(RequestOptions options = null)
            => ClientHelper.GetCurrentChannelAsync(this, options);
        /// <summary> Get information about a channel by id </summary>
        public Task<RestChannel> GetChannelAsync(ulong channelId, RequestOptions options = null)
            => ClientHelper.GetChannelAsync(this, channelId, options);
        /// <summary> Find channels relating to the specified query </summary>
        public Task<IReadOnlyCollection<RestChannel>> FindChannelAsync(string query, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.FindChannelAsync(this, query, paging, options);

        // Ingests
        /// <summary> Get information about twitch's ingest servers </summary>
        public Task<IReadOnlyCollection<RestIngest>> GetIngestsAsync(RequestOptions options = null)
            => ClientHelper.GetIngestsAsync(this, options);

        // Streams
        /// <summary> Get information about a channel's stream </summary>
        public Task<RestStream> GetStreamAsync(ulong channelId, StreamType type = StreamType.Live, RequestOptions options = null)
            => ClientHelper.GetStreamAsync(this, channelId, type, options);
        /// <summary> Get the streams for the specified channels, if available </summary>
        public Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(params ulong[] channelIds)
            => ClientHelper.GetStreamsAsync(this, channelIds);
        /// <summary> Get the top viewed streams on twitch for the specified options </summary>
        public Task<IReadOnlyCollection<RestStream>> GetStreamsAsync(Action<GetStreamsParams> parameters, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetStreamsAsync(this, parameters, paging, options);
        /// <summary> Get a summary of popularity for the specified game </summary>
        public Task<RestGameSummary> GetGameSummaryAsync(string game, RequestOptions options = null)
            => ClientHelper.GetGameSummaryAsync(this, game, options);
        /// <summary> Get the streams that appear on the front page of twitch </summary>
        public Task<IReadOnlyCollection<RestFeaturedStream>> GetFeaturedStreamsAsync(PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetFeaturedStreamsAsync(this, paging, options);
        /// <summary> Find games relating to the specified query </summary>
        public Task<IReadOnlyCollection<RestGame>> FindGamesAsync(string query, bool islive = false, RequestOptions options = null)
            => ClientHelper.FindGamesAsync(this, query, islive, options);
        /// <summary> Find streams relating to the specified query </summary>
        public Task<IReadOnlyCollection<RestStream>> FindStreamsAsync(string query, bool? hls = null, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.FindStreamsAsync(this, query, hls, paging, options);

        // Teams
        /// <summary> Get all teams on twitch </summary>
        public Task<IReadOnlyCollection<RestSimpleTeam>> GetTeamsAsync(PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetTeamsAsync(this, paging, options);
        /// <summary> Get a team by name </summary>
        public Task<RestTeam> GetTeamAsync(string name, RequestOptions options = null)
            => ClientHelper.GetTeamAsync(this, name, options);

        // Videos
        /// <summary> Get information about a video by id </summary>
        public Task<RestVideo> GetVideoAsync(string videoId, RequestOptions options = null)
            => ClientHelper.GetVideoAsync(this, videoId, options);
        /// <summary> Get the top videos on twitch based on viewcount </summary>
        public Task<IReadOnlyCollection<RestVideo>> GetTopVideosAsync(string game = null, string period = null, string broadcastType = null, string language = null, string sort = null, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetTopVideosAsync(this, game, period, broadcastType, language, sort, paging, options);
        /// <summary> Get videos from channels followed by the current user </summary>
        public Task<IReadOnlyCollection<RestVideo>> GetFollowedVideosAsync(string broadcastType = null, string language = null, string sort = null, PageOptions paging = null, RequestOptions options = null)
            => ClientHelper.GetFollowedVideosAsync(this, broadcastType, language, sort, paging, options);

        // ITwitchClient
        ConnectionState ITwitchClient.ConnectionState => ConnectionState.Disconnected;

        Task ITwitchClient.StartAsync() => Task.Delay(0);
        Task ITwitchClient.StopAsync() => Task.Delay(0);
    }
}
