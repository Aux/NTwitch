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

        internal virtual Task OnLoginAsync(bool validateToken) => Task.Delay(0);

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
            if (LoginState != LoginState.LoggedOut)
                await LogoutInternalAsync().ConfigureAwait(false);
            LoginState = LoginState.LoggingIn;

            await ApiClient.LogoutAsync().ConfigureAwait(false);

            await OnLogoutAsync().ConfigureAwait(false);
            CurrentUser = null;
            LoginState = LoginState.LoggedOut;

            await _loggedOutEvent.InvokeAsync().ConfigureAwait(false);
        }

        internal virtual Task OnLogoutAsync() => Task.Delay(0);

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
