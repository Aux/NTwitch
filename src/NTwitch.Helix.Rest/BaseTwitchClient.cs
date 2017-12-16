using System;
using System.Threading;
using System.Threading.Tasks;

namespace NTwitch.Helix.Rest
{
    public abstract class BaseTwitchClient : ITwitchClient
    {
        public event Func<LogMessage, Task> Log { add { _logEvent.Add(value); } remove { _logEvent.Remove(value); } }
        internal readonly AsyncEvent<Func<LogMessage, Task>> _logEvent = new AsyncEvent<Func<LogMessage, Task>>();

        public event Func<Task> LoggedIn { add { _loggedInEvent.Add(value); } remove { _loggedInEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _loggedInEvent = new AsyncEvent<Func<Task>>();
        public event Func<Task> LoggedOut { add { _loggedOutEvent.Add(value); } remove { _loggedOutEvent.Remove(value); } }
        private readonly AsyncEvent<Func<Task>> _loggedOutEvent = new AsyncEvent<Func<Task>>();

        internal readonly Logger _restLogger;
        private readonly SemaphoreSlim _stateLock;
        private bool _isFirstLogin, _isDisposed;

        internal API.TwitchRestApiClient ApiClient { get; }
        internal LogManager LogManager { get; }
        //public ISelfUser CurrentUser { get; protected set; }
        public LoginState LoginState { get; private set; }

        /// <summary> Creates a new REST-only github client. </summary>
        internal BaseGithubClient(TwitchRestConfig config, API.TwitchRestApiClient client)
        {
            ApiClient = client;
            LogManager = new LogManager(config.LogLevel);
            LogManager.Message += async msg => await _logEvent.InvokeAsync(msg).ConfigureAwait(false);

            _stateLock = new SemaphoreSlim(1, 1);
            _restLogger = LogManager.CreateLogger("Rest");
            _isFirstLogin = config.DisplayInitialLog;

            ApiClient.RequestQueue.RateLimitTriggered += async (id, info) =>
            {
                if (info == null)
                    await _restLogger.VerboseAsync($"Preemptive Rate limit triggered: {id ?? "null"}").ConfigureAwait(false);
                else
                    await _restLogger.WarningAsync($"Rate limit triggered: {id ?? "null"}").ConfigureAwait(false);
            };
            ApiClient.SentRequest += async (method, endpoint, millis) => await _restLogger.VerboseAsync($"{method} {endpoint}: {millis} ms").ConfigureAwait(false);
        }

        /// <inheritdoc />
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
                await OnLoginAsync(token).ConfigureAwait(false);
                LoginState = LoginState.LoggedIn;
            }
            catch (Exception)
            {
                await LogoutInternalAsync().ConfigureAwait(false);
                throw;
            }

            await _loggedInEvent.InvokeAsync().ConfigureAwait(false);
        }
        internal virtual Task OnLoginAsync(string token)
            => Task.Delay(0);

        /// <inheritdoc />
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
            if (LoginState == LoginState.LoggedOut) return;
            LoginState = LoginState.LoggingOut;

            await ApiClient.LogoutAsync().ConfigureAwait(false);

            await OnLogoutAsync().ConfigureAwait(false);
            //CurrentUser = null;
            LoginState = LoginState.LoggedOut;

            await _loggedOutEvent.InvokeAsync().ConfigureAwait(false);
        }
        internal virtual Task OnLogoutAsync()
            => Task.Delay(0);

        internal virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                ApiClient.Dispose();
                _isDisposed = true;
            }
        }
        /// <inheritdoc />
        public void Dispose() => Dispose(true);

        // IGithubClient
        ConnectionState ITwitchClient.ConnectionState => throw new NotImplementedException();

        Task ITwitchClient.StartAsync()
            => Task.Delay(0);
        Task ITwitchClient.StopAsync()
            => Task.Delay(0);
    }
}
